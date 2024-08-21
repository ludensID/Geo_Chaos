using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class ZombieAttackPreparingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _preparingZombies;
    private readonly ZombieConfig _config;
    private readonly EcsEntities _heroes;

    public ZombieAttackPreparingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _preparingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackStarted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity zombie in _preparingZombies)
      {
        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = zombie.Get<PatrolBounds>().HorizontalBounds;
        float direction = -Mathf.Sign(heroPoint - currentPoint);
        float nextPoint = currentPoint + _config.BackDistance * direction;
        nextPoint = MathUtils.Clamp(nextPoint, bounds.x, bounds.y);

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, zombie.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * _config.CalmSpeed,
          Direction = Vector2.right * direction
        });

        zombie
          .Add<AttackPreparing>()
          .Replace((ref MovePoint point) => point.Point = nextPoint);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class ZombieAttackPreparingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _preparingZombies;
    private readonly ZombieConfig _config;
    private readonly EcsEntities _heroes;

    public ZombieAttackPreparingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory,
      ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _timers = timers;
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
        float speed = zombie.Get<PatrolSpeed>().Speed;
        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        float direction = -Mathf.Sign(heroPoint - currentPoint);
        float time = _config.BackDistance / speed;

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, zombie.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * speed,
          Direction = Vector2.right * direction
        });

        zombie
          .Add<AttackPreparing>()
          .Add((ref AttackPreparingTimer timer) => timer.TimeLeft = _timers.Create(time))
          .Change((ref BodyDirection bodyDirection) => bodyDirection.Direction = -direction);
      }
    }
  }
}
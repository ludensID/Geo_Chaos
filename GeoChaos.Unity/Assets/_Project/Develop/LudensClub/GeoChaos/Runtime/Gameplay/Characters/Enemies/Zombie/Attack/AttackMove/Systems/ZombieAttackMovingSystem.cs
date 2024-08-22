using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class ZombieAttackMovingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingZombies;
    private readonly ZombieConfig _config;
    private readonly EcsEntities _heroes;

    public ZombieAttackMovingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers,
      ISpeedForceFactory forceFactory)
    {
      _timers = timers;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
      
      _movingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackPreparingFinished>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity zombie in _movingZombies)
      {
        zombie
          .Add<AttackMoving>()
          .Add((ref AttackMoveTimer timer) => timer.TimeLeft = _timers.Create(_config.AttackTime))
          .Add<StartAttackWithArmsCommand>();

        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float zombiePoint = zombie.Get<ViewRef>().View.transform.position.x;
        float direction = Mathf.Sign(heroPoint - zombiePoint);

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, zombie.PackedEntity, Vector2.right)
        {
          Speed =  Vector2.right * _config.AttackSpeed,
          Direction = Vector2.right * direction
        });

        zombie.Change((ref BodyDirection bodyDirection) => bodyDirection.Direction = direction);
      }
    }
  }
}
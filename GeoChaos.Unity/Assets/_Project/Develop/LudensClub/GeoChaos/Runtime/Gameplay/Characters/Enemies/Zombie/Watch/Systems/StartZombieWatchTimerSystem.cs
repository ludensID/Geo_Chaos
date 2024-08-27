using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class StartZombieWatchTimerSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;
    private readonly ZombieConfig _config;

    public StartZombieWatchTimerSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers,
      ISpeedForceFactory forceFactory)
    {
      _timers = timers;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _watchingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackFinished>()
        .Exc<Aimed>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies)
      {
        zombie
          .Add<StartAttackWithArmsCycleCommand>()
          .Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.WatchTime));

        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = zombie.Get<PatrolBounds>().HorizontalBounds;
        float center = (bounds.x + bounds.y) / 2;
        float direction = Mathf.Sign(center - currentPoint);

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, zombie.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * zombie.Get<PatrolSpeed>().Speed,
          Direction = Vector2.right * direction
        });

        zombie.Replace((ref MovePoint movePoint) => movePoint.Point = center);
      }
    }
  }
}
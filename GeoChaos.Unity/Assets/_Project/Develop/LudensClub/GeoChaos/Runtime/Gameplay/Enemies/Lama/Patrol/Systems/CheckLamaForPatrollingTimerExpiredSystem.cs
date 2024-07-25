using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Patrol
{
  public class CheckLamaForPatrollingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingTimers;
    private readonly LamaConfig _config;

    public CheckLamaForPatrollingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _patrollingTimers = _game
        .Filter<PatrollingTimer>()
        .Inc<LamaTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _patrollingTimers
        .Check<PatrollingTimer>(x => x.TimeLeft <= 0))
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, timer.Pack(), Vector2.right)
        {
          Instant = true
        });

        timer
          .Del<PatrollingTimer>()
          .Del<Patrolling>()
          .Add<OnPatrollFinished>()
          .Add((ref WaitingTimer lookingTimer) => lookingTimer.TimeLeft = _timers.Create(_config.LookingTime));
      }
    }
  }
}
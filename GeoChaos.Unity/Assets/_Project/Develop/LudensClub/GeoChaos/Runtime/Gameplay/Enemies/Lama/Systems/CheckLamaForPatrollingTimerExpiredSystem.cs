using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class CheckLamaForPatrollingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingTimers;

    public CheckLamaForPatrollingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory, ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;

      _patrollingTimers = _game
        .Filter<PatrollingTimer>()
        .Inc<LamaTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _patrollingTimers
        .Where<PatrollingTimer>(x => x.TimeLeft <= 0))
      {
        var ctx = timer.Get<BrainContext>().Cast<LamaContext>();

        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, timer.Pack(), Vector2.right)
        {
          Instant = true
        });

        timer
          .Del<PatrollingTimer>()
          .Del<Patrolling>()
          .Add<OnPatrolled>()
          .Add((ref LookingTimer lookingTimer) => lookingTimer.TimeLeft = _timers.Create(ctx.LookingTime));
      }
    }
  }
}
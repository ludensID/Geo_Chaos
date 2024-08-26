using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Patrol
{
  public class CheckLamaForPatrollingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingTimers;

    public CheckLamaForPatrollingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

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
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, timer.PackedEntity, Vector2.right)
        {
          Instant = true
        });

        timer
          .Del<PatrollingTimer>()
          .Del<Patrolling>()
          .Add<OnPatrolFinished>();
      }
    }
  }
}
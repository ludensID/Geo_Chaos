using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class StopPatrolToRandomPointSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _patrollingEntities;

    public StopPatrolToRandomPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _patrollingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<StopPatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _patrollingEntities)
      {
        if (entity.Has<Patrolling>())
        {
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, entity.PackedEntity);
          entity.Del<Patrolling>();
        }

        entity.Del<StopPatrolCommand>();
      }
    }
  }
}
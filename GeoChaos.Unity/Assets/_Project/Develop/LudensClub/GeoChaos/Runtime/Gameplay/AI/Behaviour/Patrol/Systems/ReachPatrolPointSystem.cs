using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class ReachPatrolPointSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingEntities;
    private readonly SpeedForceLoop _forceLoop;

    public ReachPatrolPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _patrollingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<Patrolling>()
        .Exc<FinishPatrolCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _patrollingEntities)
      {
        float currentPoint = entity.Get<ViewRef>().View.transform.position.x;
        float movePoint = entity.Get<MovePoint>().Point;
        float speed = entity.Get<MovementVector>().Speed.x;

        if (Mathf.Abs(movePoint - currentPoint) < speed * Time.fixedDeltaTime)
        {
          entity.Add<FinishPatrolCommand>();
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, entity.PackedEntity);
        }
      }
    }
  }
}
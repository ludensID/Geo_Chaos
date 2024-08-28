using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class TurnEntityNearBoundsSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingEntities;
    private readonly SpeedForceLoop _forceLoop;

    public TurnEntityNearBoundsSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<AttackMoving>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _movingEntities)
      {
        float currentPoint = entity.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = entity.Get<PatrolBounds>().HorizontalBounds;
        float speed = entity.Get<MovementVector>().Speed.x;
        ref BodyDirection bodyDirection = ref entity.Get<BodyDirection>();

        float nextPoint = currentPoint + speed * Time.fixedDeltaTime * bodyDirection.Direction;

        if (nextPoint < bounds.x || nextPoint > bounds.y)
        {
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Attack, entity.PackedEntity))
          {
            force.Change((ref MovementVector vector) => vector.Direction *= -1);
          }

          bodyDirection.Direction *= -1;
        }
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DragTargetMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _drags;
    private readonly EcsWorld _game;

    public DragTargetMovementVectorSystem(PhysicsWorldWrapper physicsWorldWrapper,
      GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _drags = _physics
        .Filter<DragForce>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity drag in _drags)
      {
        EcsEntity draggable = _game.UnpackEntity(drag.Get<Owner>().Entity);
        if (draggable.IsAlive)
        {
          float force = drag.Get<DragForce>().Force;
          draggable.Replace((ref MovementVector vector) =>
          {
            vector.Speed.x = MathUtils.DecreaseToZeroValue(vector.Speed.x, force * Time.fixedDeltaTime);
            if (vector.Direction.y > 0)
              vector.Speed.y = MathUtils.DecreaseToZeroValue(vector.Speed.y, force * Time.fixedDeltaTime);
          });
        }
      }
    }
  }
}
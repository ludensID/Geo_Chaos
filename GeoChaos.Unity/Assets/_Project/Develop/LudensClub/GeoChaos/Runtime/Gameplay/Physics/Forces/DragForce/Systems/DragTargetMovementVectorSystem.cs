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
          draggable.Replace((ref MovementVector vector) =>
          {
            vector.Speed.x -= drag.Get<DragForce>().Force * Time.fixedDeltaTime;
            vector.Speed.x = MathUtils.Clamp(vector.Speed.x, 0);
          });
        }
      }
    }
  }
}
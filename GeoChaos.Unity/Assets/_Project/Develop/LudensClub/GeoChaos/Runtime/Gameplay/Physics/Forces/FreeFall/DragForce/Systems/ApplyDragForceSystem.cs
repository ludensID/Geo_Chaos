using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class ApplyDragForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _drags;
    private readonly EcsWorld _game;

    public ApplyDragForceSystem(PhysicsWorldWrapper physicsWorldWrapper,
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
        if (_game.TryUnpackEntity(drag.Get<Owner>().Entity, out EcsEntity draggable))
        {
          Vector2 deltaForce = drag.Get<DragForce>().Force * Time.fixedDeltaTime;
          
          ref MovementVector vector = ref draggable.Get<MovementVector>();
          vector.Speed.x = MathUtils.DecreaseToZero(vector.Speed.x, deltaForce.x);
          if (vector.Direction.y > 0)
            vector.Speed.y = MathUtils.DecreaseToZero(vector.Speed.y, deltaForce.y);

          Vector2 finalSpeed = vector.Speed;
          draggable.Change((ref LastMovementVector last) => last.Speed = finalSpeed);
        }
      }
    }
  }
}
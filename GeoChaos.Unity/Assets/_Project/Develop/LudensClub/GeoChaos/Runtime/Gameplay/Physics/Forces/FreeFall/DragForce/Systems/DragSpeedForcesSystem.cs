using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DragSpeedForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _draggables;
    private readonly EcsEntities _forcables;
    private readonly EcsEntities _drags;

    public DragSpeedForcesSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _draggables = _physics
        .Filter<Draggable>()
        .Collect();

      _drags = _physics
        .Filter<DragForce>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity drag in _drags)
      {
        Vector2 force = drag.Get<DragForce>().Force;
        foreach (EcsEntity draggable in _draggables
          .Check<Owner>(x => x.Entity.EqualsTo(drag.Get<Owner>().Entity)))
        {
          ref Impact impact = ref draggable.Get<Impact>();
          ref MovementVector vector = ref draggable.Get<MovementVector>();
          for (int i = 0; i < 2; i++)
          {
            vector.Speed[i] =
              MathUtils.DecreaseToZero(vector.Speed[i], impact.Vector[i] * force[i] * Time.fixedDeltaTime);
          }
        }
      }
    }
  }
}
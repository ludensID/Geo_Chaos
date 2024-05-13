using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SetInstantZeroForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _valuables;

    public SetInstantZeroForcesSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _valuables = _physics
        .Filter<SpeedForce>()
        .Inc<Valuable>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity valuable in _valuables)
      {
        ref MovementVector vector = ref valuable.Get<MovementVector>();
        ref Impact impact = ref valuable.Get<Impact>();
        if(impact.Vector * vector.Speed * vector.Direction == Vector2.zero)
          valuable.Add<Instant>();
      }
    }
  }
}
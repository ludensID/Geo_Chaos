using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class AccelerateSpeedsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _accelerations;

    public AccelerateSpeedsSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _accelerations = _physics
        .Filter<Acceleration>()
        .Inc<MaxSpeed>()
        .Inc<MovementVector>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity accelerate in _accelerations)
      {
        ref Acceleration acceleration = ref accelerate.Get<Acceleration>();
        ref MovementVector vector = ref accelerate.Get<MovementVector>();
        ref MaxSpeed maxSpeed = ref accelerate.Get<MaxSpeed>();
        vector.Speed += acceleration.Value * Time.fixedDeltaTime;
        vector.Speed = ClampByMinToZero(vector.Speed);
        vector.Speed = Vector2.ClampMagnitude(vector.Speed, maxSpeed.Speed);
      }
    }

    private Vector2 ClampByMinToZero(Vector2 vector)
    {
      if (vector.x < 0)
        vector.x = 0;
      if (vector.y < 0)
        vector.y = 0;

      return vector;
    }
  }
}
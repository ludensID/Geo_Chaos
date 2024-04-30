using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceLoopService : ISpeedForceLoopService
  {
    private readonly PhysicsWorldWrapper _physicsWorldWrapper;

    public SpeedForceLoopService(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physicsWorldWrapper = physicsWorldWrapper;
    }

    public SpeedForceLoop CreateLoop(Action<EcsWorld.Mask> clarifier = null)
    {
      EcsWorld.Mask mask = _physicsWorldWrapper.World
        .Filter<SpeedForce>()
        .Inc<Owner>();
      
      clarifier?.Invoke(mask);
      return new SpeedForceLoop(mask.Collect());
    }

    public EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner)
    {
      return CreateLoop().GetLoop(type, owner);
    }
  }
}
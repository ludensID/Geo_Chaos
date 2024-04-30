using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface ISpeedForceLoopService
  {
    SpeedForceLoop CreateLoop(Action<EcsWorld.Mask> clarifier = null);
    EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner);
  }
}
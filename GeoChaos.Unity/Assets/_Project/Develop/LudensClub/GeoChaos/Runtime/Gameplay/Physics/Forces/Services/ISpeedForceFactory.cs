using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface ISpeedForceFactory
  {
    EcsEntity Create(SpeedForceData data);
  }
}
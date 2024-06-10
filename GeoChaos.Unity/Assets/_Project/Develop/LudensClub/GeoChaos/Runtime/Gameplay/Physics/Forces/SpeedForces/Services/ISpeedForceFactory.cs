using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public interface ISpeedForceFactory
  {
    EcsEntity Create(SpeedForceData data);
  }
}
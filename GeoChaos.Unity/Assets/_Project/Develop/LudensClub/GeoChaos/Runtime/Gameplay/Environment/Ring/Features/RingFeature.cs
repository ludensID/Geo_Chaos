using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  public class RingFeature : EcsFeature
  {
    public RingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<ReleaseRingSystem>());
      Add(systems.Create<CheckForRingReleasedSystem>());
      Add(systems.Create<SelectNearestRingSystem>());
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity
{
  public class GravityFeature : EcsFeature
  {
    public GravityFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForEntityOnGroundSystem>());
      Add(systems.Create<LandingTrackingFeature>());
    }
  }
}
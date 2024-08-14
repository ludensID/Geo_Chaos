using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking
{
  public class LandingTrackingFeature : EcsFeature
  {
    public LandingTrackingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RestartTrackLandingSystem>());
      Add(systems.Create<StartTrackLandingSystem>());
      Add(systems.Create<TrackLiftingSystem>());
      Add(systems.Create<TrackLandingSystem>());
      Add(systems.Create<StopTrackLandingSystem>());
    }
  }
}
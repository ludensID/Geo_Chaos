using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Rise
{
  public class LeafySpiritRisingFeature : EcsFeature
  {
    public LeafySpiritRisingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DeleteLeafySpiritRiseStartedEventSystem>());
      Add(systems.Create<LeafySpiritRiseSystem>());
      Add(systems.Create<CheckForLeafySpiritRiseTimerExpiredSystem>());

      Add(systems.Create<EnableLeafySpiritBodyColliderSystem>());
      Add(systems.Create<ActivateLeafySpiritBodySystem>());
    }
  }
}
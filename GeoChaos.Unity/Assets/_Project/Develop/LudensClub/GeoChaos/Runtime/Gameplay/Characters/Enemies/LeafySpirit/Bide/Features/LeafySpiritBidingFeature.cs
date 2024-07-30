using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide
{
  public class LeafySpiritBidingFeature : EcsFeature
  {
    public LeafySpiritBidingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritBidingSystem>());
      Add(systems.Create<DeleteLeafySpiritBidingFinishedEventSystem>());
      Add(systems.Create<CheckForLeafySpiritBidingTimerExpiredSystem>());
      Add(systems.Create<StopLeafySpiritBidingSystem>());
    }
  }
}
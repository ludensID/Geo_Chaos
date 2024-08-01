using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Rise
{
  public class LeafySpiritRisingFeature : EcsFeature
  {
    public LeafySpiritRisingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritRiseSystem>());
      Add(systems.Create<DeleteLeafySpiritRiseFinishedEventSystem>());
      Add(systems.Create<CheckForLeafySpiritRiseTimerExpiredSystem>());
    }
  }
}
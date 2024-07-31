using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class LeafySpiritWatchFeature : EcsFeature
  {
    public LeafySpiritWatchFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritWatchingAfterLoseTargetSystem>());
      Add(systems.Create<DeleteWatchingTimerOnAimedLeafySpiritSystem>());
      Add(systems.Create<DeleteExpiredLeafySpiritWatchingTimerSystem>());
    }
  }
}
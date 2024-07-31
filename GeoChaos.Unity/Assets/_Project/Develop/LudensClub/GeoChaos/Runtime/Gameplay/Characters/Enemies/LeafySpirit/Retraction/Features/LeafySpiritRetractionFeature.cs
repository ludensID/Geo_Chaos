using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction
{
  public class LeafySpiritRetractionFeature : EcsFeature
  {
    public LeafySpiritRetractionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritRetractionSystem>());
      Add(systems.Create<DeleteLeafySpiritRetractionFinishedEventSystem>());
      Add(systems.Create<FinishLeafySpiritRetractionSystem>());
      Add(systems.Create<StopLeafySpiritRetractionSystem>());
    }
  }
}
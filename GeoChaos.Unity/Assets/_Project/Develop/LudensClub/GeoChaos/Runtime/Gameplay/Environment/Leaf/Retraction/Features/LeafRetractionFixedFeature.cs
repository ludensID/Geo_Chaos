using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class LeafRetractionFixedFeature : EcsFeature
  {
    public LeafRetractionFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CalculateLeafRetractionDirectionSystem>());
      Add(systems.Create<CheckForLeafRetractedSystem>());
    }
  }
}
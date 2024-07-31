using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class LeafRetractionFeature : EcsFeature
  {
    public LeafRetractionFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RetractLeafSystem>());
    } 
  }
}
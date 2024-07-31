using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class LeafFixedFeature : EcsFeature
  {
    public LeafFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafMovingFixedFeature>());
      Add(systems.Create<LeafRetractionFixedFeature>());
    }
  }
}
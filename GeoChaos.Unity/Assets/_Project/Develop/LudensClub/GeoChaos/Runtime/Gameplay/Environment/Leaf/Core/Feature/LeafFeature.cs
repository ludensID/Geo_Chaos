using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class LeafFeature : EcsFeature
  {
    public LeafFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafMovingFeature>());
      Add(systems.Create<LeafRetractionFeature>());
    }
  }
}
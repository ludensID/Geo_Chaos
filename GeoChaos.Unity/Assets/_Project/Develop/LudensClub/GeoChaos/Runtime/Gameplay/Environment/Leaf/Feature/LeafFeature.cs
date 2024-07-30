using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class LeafFeature : EcsFeature
  {
    public LeafFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<MoveLeafSystem>());
    }
  }
}
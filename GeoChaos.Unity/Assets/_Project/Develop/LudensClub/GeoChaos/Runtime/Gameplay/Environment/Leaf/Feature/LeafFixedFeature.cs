using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class LeafFixedFeature : EcsFeature
  {
    public LeafFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForLeafReachedPositionSystem>());
      Add(systems.Create<StopMoveLeafSystem>());
    }
  }
}
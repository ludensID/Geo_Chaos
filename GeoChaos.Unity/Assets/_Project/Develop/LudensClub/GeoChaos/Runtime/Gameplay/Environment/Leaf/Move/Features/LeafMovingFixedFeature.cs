using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move
{
  public class LeafMovingFixedFeature : EcsFeature
  {
    public LeafMovingFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckForLeafReachedPositionSystem>());
      Add(systems.Create<StopMoveLeafSystem>());
    } 
  }
}
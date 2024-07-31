using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move
{
  public class LeafMovingFeature : EcsFeature
  {
    public LeafMovingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<MoveLeafSystem>());
    } 
  }
}
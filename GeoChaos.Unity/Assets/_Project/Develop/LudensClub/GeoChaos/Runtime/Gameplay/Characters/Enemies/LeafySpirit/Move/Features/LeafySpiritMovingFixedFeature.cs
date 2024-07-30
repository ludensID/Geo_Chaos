using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class LeafySpiritMovingFixedFeature : EcsFeature
  {
    public LeafySpiritMovingFixedFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<MoveLeafySpiritSystem>());
      Add(systems.Create<CheckForLeafySpiritReachPositionSystem>());
    }
  }
}
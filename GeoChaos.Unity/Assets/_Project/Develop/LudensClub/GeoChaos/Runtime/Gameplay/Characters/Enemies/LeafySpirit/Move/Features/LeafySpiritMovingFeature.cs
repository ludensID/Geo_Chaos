using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class LeafySpiritMovingFeature : EcsFeature
  {
    public LeafySpiritMovingFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartMoveLeafySpiritSystem>());
      Add(systems.Create<FinishMoveLeafySpiritSystem>());
      Add(systems.Create<StopMoveLeafySpiritSystem>());
    }
  }
}
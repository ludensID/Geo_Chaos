using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class LeafySpiritRelaxationFeature : EcsFeature
  {
    public LeafySpiritRelaxationFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<LeafySpiritRelaxationSystem>());
      Add(systems.Create<DeleteLeafySpiritRelaxationFinishedEventSystem>());
      Add(systems.Create<FinishLeafySpiritRelaxationSystem>());
      Add(systems.Create<StopLeafySpiritRelaxationSystem>());
    }
  }
}
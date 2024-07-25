using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Leap
{
  public class LeafySpiritLeapFeature : EcsFeature
  {
    public LeafySpiritLeapFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<PrecastLeafySpiritLeapSystem>());
      Add(systems.Create<LeafySpiritLeapSystem>());
      Add(systems.Create<DeleteLeafySpiritLeapFinishedEventSystem>());
      Add(systems.Create<FinishLeafySpiritLeapSystem>());
      Add(systems.Create<StopLeafySpiritLeapSystem>());
    } 
  }
}
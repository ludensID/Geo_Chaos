using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Lever
{
  public class LeverFeature : EcsFeature
  {
    public LeverFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DetectHeroNearLeverSystem>());
      Add(systems.Create<InteractWithLeverSystem>());
    }
  }
}
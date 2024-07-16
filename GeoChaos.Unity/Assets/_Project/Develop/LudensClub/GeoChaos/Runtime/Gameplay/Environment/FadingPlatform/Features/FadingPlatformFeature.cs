using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class FadingPlatformFeature : EcsFeature
  {
    public FadingPlatformFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DetectHeroOnFadingPlatformSystem>());
      
      Add(systems.Create<StartFadingPlatformSystem>());
      Add(systems.Create<DeleteFadingPlatformStartFadeCommandSystem>());
      Add(systems.Create<MakePlatformFadedSystem>());
      Add(systems.Create<MakePlatformAppearedSystem>());
      
      Add(systems.Create<SetFadingPlatformColliderSystem>());
    }
  }
}
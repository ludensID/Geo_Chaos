using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class DeleteFadingPlatformStartFadeCommandSystem : Delete<StartFadeCommand, GameWorldWrapper>
  {
    protected DeleteFadingPlatformStartFadeCommandSystem(GameWorldWrapper wrapper)
      : base(wrapper, x => x.Inc<FadingPlatformTag>())
    {
    }
  }
}
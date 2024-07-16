using LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class EnvironmentFeature : EcsFeature
  {
    public EnvironmentFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DamageFromSpikeSystem>());

      Add(systems.Create<FadingPlatformFeature>());
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.HealthShard;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Lever;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class EnvironmentFeature : EcsFeature
  {
    public EnvironmentFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<DamageFromSpikeSystem>());

      Add(systems.Create<FadingPlatformFeature>());
      Add(systems.Create<KeyFeature>());
      Add(systems.Create<LeverFeature>());
      Add(systems.Create<DoorFeature>());
      
      Add(systems.Create<HealthShardFeature>());
    }
  }
}
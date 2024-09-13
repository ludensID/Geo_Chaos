using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.HealthShard;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Lever;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Spike;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class EnvironmentFeature : EcsFeature
  {
    public EnvironmentFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CheckpointFeature>());
      Add(systems.Create<RingFeature>());

      Add(systems.Create<DamageFromSpikeSystem>());

      Add(systems.Create<FadingPlatformFeature>());
      Add(systems.Create<KeyFeature>());
      Add(systems.Create<LeverFeature>());
      Add(systems.Create<DoorFeature>());

      Add(systems.Create<HealthShardFeature>());

      Add(systems.Create<LeafFeature>());
      Add(systems.Create<TongueFeature>());
      Add(systems.Create<GasCloudFeature>());
    }
  }
}
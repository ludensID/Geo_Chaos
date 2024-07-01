using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class EnvironmentFeature : EcsFeature
  {
    public EnvironmentFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<CreateRingSystem>());
      Add(systems.Create<CreateSpikeSystem>());
      
      Add(systems.Create<DamageFromSpikeSystem>());
    }
  }
}
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class GasCloudFeature : EcsFeature
  {
    public GasCloudFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<StartExpandGasCloudSystem>());
      Add(systems.Create<ExpandGasCloudSystem>());
      Add(systems.Create<CheckForGasCloudLifeTimeExpiredSystem>());
      
      Add(systems.Create<DamageFromGasCloudSystem>());
      
      Add(systems.Create<SetGasCloudScaleSystem>());
    }
  }
}
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class ShroomGasShootingCycleFeature : EcsFeature
  {
    public ShroomGasShootingCycleFeature(IEcsSystemFactory systems)
    {
      Add(systems.Create<RestartGasShootingCycleSystem>());
      Add(systems.Create<StartGasShootingCycleSystem>());
      
      Add(systems.Create<DeleteExpiredGasShootingCooldownSystem>());
      Add(systems.Create<DeleteGasShotEventSystem>());
      Add(systems.Create<ShootGasSystem>());

      Add(systems.Create<StopGasShootingCycleSystem>());
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class RestartGasShootingCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;

    public RestartGasShootingCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _shootingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<StartGasShootingCycleCommand>()
        .Inc<StopGasShootingCycleCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _shootingShrooms)
      {
        shroom
          .Del<StopGasShootingCycleCommand>()
          .Has<GasShootingCooldown>(false)
          .Has<ShootingGas>(false);
      }
    }
  }
}
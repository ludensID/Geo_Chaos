using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class StopGasShootingCycleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;

    public StopGasShootingCycleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _shootingShrooms = _game
        .Filter<ShroomTag>()
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
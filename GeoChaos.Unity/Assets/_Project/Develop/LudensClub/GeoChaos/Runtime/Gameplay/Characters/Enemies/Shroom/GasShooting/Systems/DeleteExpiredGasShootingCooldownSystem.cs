using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class DeleteExpiredGasShootingCooldownSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;

    public DeleteExpiredGasShootingCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _shootingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<GasShootingCooldown>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _shootingShrooms
        .Check<GasShootingCooldown>(x => x.TimeLeft <= 0))
      {
        shroom.Del<GasShootingCooldown>();
      }
    }
  }
}
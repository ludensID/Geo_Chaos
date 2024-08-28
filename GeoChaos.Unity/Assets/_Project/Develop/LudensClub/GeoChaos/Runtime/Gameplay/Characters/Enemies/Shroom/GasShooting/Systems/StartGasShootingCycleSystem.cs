using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class StartGasShootingCycleSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _shootingShrooms;

    public StartGasShootingCycleSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;

      _shootingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<StartGasShootingCycleCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _shootingShrooms)
      {
        shroom
          .Del<StartGasShootingCycleCommand>()
          .Add<ShootingGas>()
          .Add((ref GasShootingCooldown cooldown) =>
            cooldown.TimeLeft = _timers.Create(shroom.Get<GasShootingCooldownTime>().Time));;
      }
    }
  }
}
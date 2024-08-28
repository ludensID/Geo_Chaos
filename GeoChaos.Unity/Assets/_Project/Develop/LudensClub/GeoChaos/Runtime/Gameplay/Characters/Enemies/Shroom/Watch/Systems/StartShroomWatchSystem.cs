using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Systems
{
  public class StartShroomWatchSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingShrooms;
    private readonly ShroomConfig _config;

    public StartShroomWatchSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ShroomConfig>();

      _watchingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<OnAttackFinished>()
        .Exc<Aimed>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _watchingShrooms)
      {
        shroom
          .Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.PlayerWaitTime))
          .Add<StartGasShootingCycleCommand>()
          .Replace((ref GasShootingCooldownTime time) => time.Time = _config.WaitShotCooldown);
      }
    }
  }
}
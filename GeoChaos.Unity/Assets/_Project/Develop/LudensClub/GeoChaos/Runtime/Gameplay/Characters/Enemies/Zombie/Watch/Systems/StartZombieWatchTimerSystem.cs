using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class StartZombieWatchTimerSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;
    private readonly ZombieConfig _config;

    public StartZombieWatchTimerSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();
      
      _watchingZombies = _game
        .Filter<ZombieTag>()
        .Inc<OnAttackFinished>()
        .Exc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies)
      {
        zombie.Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.WatchTime));
      }
    }
  }
}
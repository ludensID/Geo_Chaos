using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Wait;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Wait
{
  public class ZombieWaitingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly ZombieConfig _config;
    private readonly EcsEntities _waitingZombies;

    public ZombieWaitingSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();

      _waitingZombies = _game
        .Filter<ZombieTag>()
        .Inc<WaitCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _waitingZombies)
      {
        zombie
          .Del<WaitCommand>()
          .Add((ref WaitingTimer timer) => timer.TimeLeft = _timers.Create(_config.WaitTime));
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class DeleteExpiredZombieWatchTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;

    public DeleteExpiredZombieWatchTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingZombies = _game
        .Filter<ZombieTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        zombie.Del<WatchingTimer>();
      } 
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Watch
{
  public class CheckLamaForWatchingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _expiredTimers;

    public CheckLamaForWatchingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _expiredTimers = _game
        .Filter<LamaTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _expiredTimers
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        timer.Del<WatchingTimer>();
      }
    }
  }
}
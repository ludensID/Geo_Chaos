using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class DeleteExpiredFrogWatchingTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingFrogs;

    public DeleteExpiredFrogWatchingTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _watchingFrogs
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        frog.Del<WatchingTimer>();
      }
    }
  }
}
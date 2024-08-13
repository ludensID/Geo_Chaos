using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class DeleteExpiredLeafySpiritWatchingTimerSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingSpirits;

    public DeleteExpiredLeafySpiritWatchingTimerSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _watchingSpirits
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        spirit.Del<WatchingTimer>();
      }
    }
  }
}
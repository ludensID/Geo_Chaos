using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Systems
{
  public class StopShroomWatchWhenTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;

    public StopShroomWatchWhenTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingZombies = _game
        .Filter<ShroomTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        zombie
          .Del<WatchingTimer>()
          .Has<StopGasShootingCycleCommand>(true);
      } 
    }
  }
}
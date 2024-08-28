using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Watch.Systems
{
  public class StopAimedShroomWatchSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingShrooms;

    public StopAimedShroomWatchSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _watchingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<WatchingTimer>()
        .Inc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _watchingShrooms)
      {
        shroom
          .Del<WatchingTimer>()
          .Has<StopGasShootingCycleCommand>(true);
      }
    }
  }
}
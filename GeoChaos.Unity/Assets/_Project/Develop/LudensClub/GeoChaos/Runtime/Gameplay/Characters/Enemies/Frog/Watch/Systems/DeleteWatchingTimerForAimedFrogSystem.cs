using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class DeleteWatchingTimerForAimedFrogSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingFrogs;

    public DeleteWatchingTimerForAimedFrogSystem(GameWorldWrapper gameWorldWrapper) 
    {
      _game = gameWorldWrapper.World;

      _watchingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _watchingFrogs)
      {
        if (frog.Has<TargetInView>() || frog.Has<TargetInFront>())
          frog.Del<WatchingTimer>();
      } 
    }
  }
}
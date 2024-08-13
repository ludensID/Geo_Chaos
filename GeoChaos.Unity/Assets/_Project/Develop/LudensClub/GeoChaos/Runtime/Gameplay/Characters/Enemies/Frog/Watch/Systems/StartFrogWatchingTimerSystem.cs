using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.JumpWait;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Watch
{
  public class StartFrogWatchingTimerSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly FrogConfig _config;
    private readonly EcsEntities _watchingFrogs;

    public StartFrogWatchingTimerSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _watchingFrogs = _game
        .Filter<FrogTag>()
        .Inc<WatchCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _watchingFrogs)
      {
        if (!frog.Has<Attacking>() && !frog.Has<Jumping>() || frog.Has<JumpWaitTimer>())
        {
          frog
            .Del<WatchCommand>()
            .Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.WatchTime));
        }
      }
    }
  }
}
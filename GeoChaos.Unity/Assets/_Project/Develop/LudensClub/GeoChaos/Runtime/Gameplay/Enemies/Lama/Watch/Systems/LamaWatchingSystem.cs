﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Watch
{
  public class LamaWatchingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;
    private readonly SpeedForceLoop _forceLoop;
    private readonly LamaConfig _config;

    public LamaWatchingSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      ISpeedForceLoopService forceLoopSvc,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();
      
      _forceLoop = forceLoopSvc.CreateLoop(x => 
        x.Exc<SpeedForceCommand>());

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<WatchCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if (!lama.Has<Watching>())
        {
          lama
            .Add<Watching>()
            .Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.ListenTime));

          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Chase, lama.Pack()))
          {
            force.Replace((ref SpeedForce speedForce) => speedForce.Type = SpeedForceType.Sneak);
          }
        }

        lama.Del<WatchCommand>();
      }
    }
  }
}
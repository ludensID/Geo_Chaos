﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Watch
{
  public class StopLamaWatchingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public StopLamaWatchingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<StopWatchCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        if (lama.Has<Watching>())
        {
          lama
            .Del<Watching>()
            .Has<WatchingTimer>(false);

          _forceFactory.Create(new SpeedForceData(SpeedForceType.Sneak, lama.Pack(), Vector2.right)
          {
            Instant = true
          });
        }

        lama.Del<StopWatchCommand>();
      }
    }
  }
}
﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class FinishHookInterruptionSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _interruptedHooks;
    private readonly EcsEntities _selectedRings;
    private readonly HeroConfig _config;

    public FinishHookInterruptionSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();
      
      _interruptedHooks = _game
        .Filter<OnHookInterrupted>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hook in _interruptedHooks)
      {
        hook
          .Add<UnlockMovementCommand>()
          .Del<Hooking>();
          
        foreach (EcsEntity ring in _selectedRings)
        {
          ring
            .Del<Hooked>()
            .Add((ref Releasing releasing) => releasing.TimeLeft = _timers.Create(_config.RingReleasingTime));
        }
      }
    }
  }
}
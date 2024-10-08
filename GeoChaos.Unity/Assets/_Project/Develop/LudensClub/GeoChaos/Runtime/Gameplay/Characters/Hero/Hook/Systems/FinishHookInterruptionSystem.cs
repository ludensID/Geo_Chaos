﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class FinishHookInterruptionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interruptedHooks;

    public FinishHookInterruptionSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _interruptedHooks = _game
        .Filter<HeroTag>()
        .Inc<OnHookInterrupted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hook in _interruptedHooks)
      {
        hook
          .Del<Hooking>()
          .Del<StopHookCommand>();
        
        ref MovementLayout layout = ref hook.Get<MovementLayout>();
        if (layout.Movement == MovementType.Hook)
        {
          layout.Layer = MovementLayer.All;
          layout.Movement = MovementType.None;
        }
      }
    }
  }
}
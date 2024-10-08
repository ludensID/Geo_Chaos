﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook
{
  public class DeleteHookInputCooldownSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _cooldowns;

    public DeleteHookInputCooldownSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _cooldowns = _game
        .Filter<HeroTag>()
        .Inc<HookInputCooldown>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cooldown in _cooldowns
        .Check<HookInputCooldown>(x => x.TimeLeft <= 0))
      {
        cooldown.Del<HookInputCooldown>();
      }
    }
  }
}
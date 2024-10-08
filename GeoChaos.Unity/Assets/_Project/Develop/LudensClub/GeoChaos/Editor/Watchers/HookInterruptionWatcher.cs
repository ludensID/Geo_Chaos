﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class HookInterruptionWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly Runtime.Configuration.HeroConfig _config;
    private bool _allowHookInterruption;
    private readonly EcsEntities _heroes;

    public HookInterruptionWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

      _allowHookInterruption = _config.AllowHookInterruption;
      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
    }

    public bool IsDifferent()
    {
      return _allowHookInterruption != _config.AllowHookInterruption;
    }

    public void Assign()
    {
      _allowHookInterruption = _config.AllowHookInterruption;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
        hero.Has<InterruptHookAvailable>(_allowHookInterruption);
    }
  }
}
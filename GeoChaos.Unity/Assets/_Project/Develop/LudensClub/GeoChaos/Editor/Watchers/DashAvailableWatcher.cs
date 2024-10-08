﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class DashAvailableWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly Runtime.Configuration.HeroConfig _config;
    private readonly EcsEntities _heroes;
    private bool _dashAvailable;

    public DashAvailableWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _dashAvailable = _config.EnableDash;
    }
    
    public bool IsDifferent()
    {
      return _dashAvailable != _config.EnableDash;
    }

    public void Assign()
    {
      _dashAvailable = _config.EnableDash;
    }

    public void OnChanged()
    {
      foreach (EcsEntity hero in _heroes)
        hero.Has<DashAvailable>(_dashAvailable);
    }
  }
}
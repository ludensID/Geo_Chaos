﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Editor.Watchers
{
  public class GravityScaleWatcher : IWatcher
  {
    private readonly EcsWorld _game;
    private readonly Runtime.Configuration.HeroConfig _config;
    private readonly EcsEntities _heroes;
    private float _gravityScale;

    public GravityScaleWatcher(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<Runtime.Configuration.HeroConfig>();

      _gravityScale = _config.GravityScale;
      _heroes = _game
        .Filter<HeroTag>()
        .Inc<GravityScale>()
        .Collect();
    }

    public bool IsDifferent()
    {
      return _gravityScale.ApproximatelyEqual(_config.GravityScale);
    }

    public void Assign()
    {
      _gravityScale = _config.GravityScale;
    }

    public void OnChanged()
    {
      if (!float.IsFinite(_gravityScale))
        return;

      foreach (EcsEntity hero in _heroes)
      {
        hero.Change((ref GravityScale gravity) =>
          gravity.Scale.Value = hero.Has<Falling>() ? _config.FallGravityScale : _config.GravityScale);
      }
    }
  }
}
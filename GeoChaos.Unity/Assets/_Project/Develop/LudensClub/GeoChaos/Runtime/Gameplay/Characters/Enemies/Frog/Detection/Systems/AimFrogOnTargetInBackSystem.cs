﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class AimFrogOnTargetInBackSystem : IEcsRunSystem
  {
    private readonly FrogTargetInBackSelector _selector;
    private readonly EcsWorld _game;
    private readonly FrogConfig _config;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _frogs;
    private readonly EcsEntities _markedFrogs;

    public AimFrogOnTargetInBackSystem(GameWorldWrapper gameWorldWrapper,
      FrogTargetInBackSelector selector,
      IConfigProvider configProvider)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _frogs = _game
        .Filter<FrogTag>()
        .Collect();

      _markedFrogs = _game
        .Filter<FrogTag>()
        .Inc<Marked>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity frog in _frogs)
      {
        frog.Replace((ref ViewRadius radius) => radius.Radius = _config.BackRadius);
      }

      _selector.Select<TargetInBack>(_heroes, _frogs, _markedFrogs);
    }
  }
}
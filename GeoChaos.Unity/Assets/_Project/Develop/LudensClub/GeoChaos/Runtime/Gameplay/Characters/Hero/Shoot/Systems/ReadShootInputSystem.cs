﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class ReadShootInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _shootables;
    private readonly EcsEntities _inputs;

    public ReadShootInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _shootables = _game
        .Filter<HeroTag>()
        .Inc<ShootAvailable>()
        .Collect();

      _inputs = _input
        .Filter<IsShoot>()
        .Inc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity shoot in _shootables)
      {
        shoot.Add<ShootCommand>();
      }
    }
  }
}
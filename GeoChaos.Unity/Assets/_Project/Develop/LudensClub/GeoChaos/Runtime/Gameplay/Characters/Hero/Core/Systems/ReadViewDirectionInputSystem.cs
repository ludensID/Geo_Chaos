﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class ReadViewDirectionInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadViewDirectionInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<Movable>()
        .Inc<ViewDirection>()
        .Collect();

      _inputs = _input
        .Filter<HorizontalMovement>()
        .Inc<VerticalMovement>()
        .Inc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity hero in _heroes)
      {
        var direction = new Vector2(input.Get<HorizontalMovement>().Direction,
          input.Get<VerticalMovement>().Direction);
        if (direction == Vector2.zero)
          direction = Vector2.right * hero.Get<BodyDirection>().Direction;

        hero.Change((ref ViewDirection viewDir) => viewDir.Direction = direction);
      }
    }
  }
}
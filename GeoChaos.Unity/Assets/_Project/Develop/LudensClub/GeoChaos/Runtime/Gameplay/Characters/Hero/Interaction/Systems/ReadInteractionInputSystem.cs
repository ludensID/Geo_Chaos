﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Interaction
{
  public class ReadInteractionInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadInteractionInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _inputs = _input
        .Filter<Expired>()
        .Inc<IsInteraction>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity hero in _heroes
        .Check<MovementLayout>(x => x.Layer == MovementLayer.All))
      {
        hero.Add<InteractCommand>();
      }
    }
  }
}
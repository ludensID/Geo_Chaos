﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Attack
{
  public class ReadAttackInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsFilter _heroes;
    private readonly EcsFilter _inputs;

    public ReadAttackInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<AttackAvailable>()
        .Exc<IsMovementLocked>()
        .End();

      _inputs = _input
        .Filter<Expired>()
        .Inc<IsAttack>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int _ in _inputs)
      foreach (int hero in _heroes)
        _game.Add<AttackCommand>(hero);
    }
  }
}
﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
  public class SetHeroSwordViewColorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;

    public SetHeroSwordViewColorSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<HeroSwordViewRef>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Change((ref HeroSwordViewRef sword) =>
        {
          Color color = sword.View.DefaultColor;
          if (hero.Has<Attacking>())
            color = sword.View.AttackColor;

          if (hero.Has<ComboAttackTimer>())
            color = sword.View.ComboColor;

          sword.View.SetColor(color);
        });
      }
    }
  }
}
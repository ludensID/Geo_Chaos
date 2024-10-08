﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Initialize
{
  public class InitializeHeroRigidbodySystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _heroes;

    public InitializeHeroRigidbodySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<HeroTag>()
        .Inc<GravityScale>()
        .Inc<OnConverted>()
        .Inc<RigidbodyRef>()
        .Collect();
    }

    public void Init(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Change((ref RigidbodyRef rigidbodyRef) =>
          rigidbodyRef.Rigidbody.gravityScale = hero.Get<GravityScale>().Scale);
      }
    }
  }
}
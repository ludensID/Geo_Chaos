using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  [Obsolete]
  public class SelectRingsDirectedToHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _selectedRings;
    private readonly HeroConfig _config;

    public SelectRingsDirectedToHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();

      _selectedRings = _game
        .Filter<RingTag>()
        .Inc<ViewRef>()
        .Inc<Selected>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity ring in _selectedRings)
      {
        Transform heroTransform = hero.Get<ViewRef>().View.transform;
        Transform ringTransform = ring.Get<ViewRef>().View.transform;
        float deltaX = ringTransform.position.x - heroTransform.position.x;
        float ringDirection = ringTransform.right.x;
        float heroDirection = heroTransform.right.x;
        if (Mathf.Sign(deltaX) * Mathf.Sign(ringDirection) > 0
          || Mathf.Sign(ringDirection) * Mathf.Sign(heroDirection) > 0
          || Mathf.Abs(deltaX) < _config.RingHorizontalDistance)
          ring.Del<Selected>();
      }
    }
  }
}
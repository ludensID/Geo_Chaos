﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _dashViews;

    public DashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _dashViews = _world
        .Filter<DashCommand>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var view in _dashViews)
      {
        view
          .Replace((ref DashColliderRef collider) => collider.Collider.enabled = true)
          .Replace((ref ColliderRef collider) => collider.Collider.enabled = false);
      }
    }
  }
}
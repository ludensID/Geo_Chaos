﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class StopDashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _dashViews;

    public StopDashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _dashViews = _world
        .Filter<StopDashCommand>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity view in _dashViews)
      {
        view
          .Replace((ref DashColliderRef collider) => collider.Collider.enabled = false)
          .Replace((ref ColliderRef collider) => collider.Collider.enabled = true);
      }
    }
  }
}
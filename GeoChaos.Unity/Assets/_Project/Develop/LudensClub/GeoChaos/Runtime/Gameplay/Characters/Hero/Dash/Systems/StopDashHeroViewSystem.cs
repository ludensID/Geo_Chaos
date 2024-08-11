using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash
{
  public class StopDashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _dashViews;

    public StopDashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _dashViews = _world
        .Filter<HeroTag>()
        .Inc<StopDashCommand>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity view in _dashViews)
      {
        view
          .Change((ref DashColliderRef collider) => collider.Collider.enabled = false)
          .Change((ref ColliderRef collider) => collider.Collider.enabled = true);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash
{
  public class DashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _dashViews;

    public DashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _dashViews = _game
        .Filter<HeroTag>()
        .Inc<DashCommand>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var view in _dashViews)
      {
        view
          .Change((ref DashColliderRef collider) => collider.Collider.enabled = true)
          .Change((ref ColliderRef collider) => collider.Collider.enabled = false);
      }
    }
  }
}
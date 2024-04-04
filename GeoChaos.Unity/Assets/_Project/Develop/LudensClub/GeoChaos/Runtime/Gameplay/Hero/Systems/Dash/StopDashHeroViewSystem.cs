using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class StopDashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public StopDashHeroViewSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world
        .Filter<StopDashCommand>()
        .Inc<RigidbodyRef>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref DashColliderRef dashColliderRef = ref _world.Get<DashColliderRef>(hero);
        dashColliderRef.Collider.enabled = false;

        ref ColliderRef colliderRef = ref _world.Get<ColliderRef>(hero);
        colliderRef.Collider.enabled = true;
        
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        rigidbodyRef.Rigidbody.gravityScale = _config.GravityScale;
      }
    }
  }
}
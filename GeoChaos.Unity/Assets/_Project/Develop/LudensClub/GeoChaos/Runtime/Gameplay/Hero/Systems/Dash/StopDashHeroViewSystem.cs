using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class StopDashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public StopDashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

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
        rigidbodyRef.Rigidbody.gravityScale = 1;
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public DashHeroViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<DashCommand>()
        .Inc<RigidbodyRef>()
        .Inc<DashColliderRef>()
        .Inc<ColliderRef>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var dashColliderRef = ref _world.Get<DashColliderRef>(hero);
        dashColliderRef.Collider.enabled = true;

        ref var colliderRef = ref _world.Get<ColliderRef>(hero);
        colliderRef.Collider.enabled = false;

        ref var rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        rigidbodyRef.Rigidbody.gravityScale = 0;
      }
    }
  }
}
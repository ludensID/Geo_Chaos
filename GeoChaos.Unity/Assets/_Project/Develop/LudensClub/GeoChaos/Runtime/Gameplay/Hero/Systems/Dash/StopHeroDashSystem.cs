using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class StopHeroDashSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public StopHeroDashSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<DashAvailable>()
        .Inc<IsDashing>()
        .Inc<StopDashCommand>()
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
        
        // TODO: store gravity scale
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        rigidbodyRef.Rigidbody.gravityScale = 1;

        _world.Add<Movable>(hero);
        _world.Add<JumpAvailable>(hero);

        _world.Del<IsDashing>(hero);
        _world.Del<StopDashCommand>(hero);
      }
    }
  }
}
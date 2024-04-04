using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroGravitySystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly HeroConfig _config;
    private readonly EcsFilter _heroes;

    public InitializeHeroGravitySystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world
        .Filter<Hero>()
        .Inc<RigidbodyRef>()
        .End();
    }
    
    public void Init(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        rigidbodyRef.Rigidbody.gravityScale = _config.GravityScale;
      }
    }
  }
}
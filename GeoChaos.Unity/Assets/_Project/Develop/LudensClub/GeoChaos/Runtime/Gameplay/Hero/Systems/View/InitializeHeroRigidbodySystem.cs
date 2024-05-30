using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroRigidbodySystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _heroes;

    public InitializeHeroRigidbodySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<GravityScale>()
        .Inc<OnConverted>()
        .Inc<RigidbodyRef>()
        .Collect();
    }

    public void Init(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Replace((ref RigidbodyRef rigidbodyRef) =>
          rigidbodyRef.Rigidbody.gravityScale = hero.Get<GravityScale>().Value);
      }
    }
  }
}
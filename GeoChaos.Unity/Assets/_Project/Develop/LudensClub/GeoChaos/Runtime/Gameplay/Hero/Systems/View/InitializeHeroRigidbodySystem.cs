using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroRigidbodySystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public InitializeHeroRigidbodySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<GravityScale>()
        .Inc<OnConverted>()
        .Inc<RigidbodyRef>()
        .End();
    }

    public void Init(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var gravityScale = ref _world.Get<GravityScale>(hero);
        ref var rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);
        rigidbodyRef.Rigidbody.gravityScale = gravityScale.Value;
      }
    }
  }
}
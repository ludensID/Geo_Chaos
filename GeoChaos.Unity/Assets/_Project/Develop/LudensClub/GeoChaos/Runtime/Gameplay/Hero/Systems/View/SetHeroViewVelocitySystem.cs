using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetHeroViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public SetHeroViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _heroes = _world.Filter<Hero>()
        .Inc<RigidbodyRef>()
        .Inc<HeroVelocity>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var velocity = ref _world.Get<HeroVelocity>(hero);
        ref var rigidbodyRef = ref _world.Get<RigidbodyRef>(hero);

        rigidbodyRef.Rigidbody.velocity = velocity.Velocity;
      }
    }
  }
}
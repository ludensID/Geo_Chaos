using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class SetViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;

    public SetViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _heroes = _game
        .Filter<RigidbodyRef>()
        .Inc<Velocity>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        hero.Replace((ref RigidbodyRef rigidbodyRef) => rigidbodyRef.Rigidbody.velocity = hero.Get<Velocity>().Value);
      }
    }
  }
}
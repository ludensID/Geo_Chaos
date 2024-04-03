using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadHeroViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _vectors;

    public ReadHeroViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _vectors = _world.Filter<RigidbodyRef>()
        .Inc<HeroMovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int vector in _vectors)
      {
        ref RigidbodyRef rigidbodyRef = ref _world.Get<RigidbodyRef>(vector);
        ref HeroMovementVector movementVector = ref _world.Get<HeroMovementVector>(vector);
        Vector2 velocity = rigidbodyRef.Rigidbody.velocity;
        movementVector.Speed.y = Mathf.Abs(velocity.y);
        movementVector.Direction.y = Mathf.Sign(velocity.y);
      }
    }
  }
}
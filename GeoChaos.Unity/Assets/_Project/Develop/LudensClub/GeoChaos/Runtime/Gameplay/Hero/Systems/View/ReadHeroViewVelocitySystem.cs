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
        .Inc<MovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var vector in _vectors)
      {
        ref var rigidbodyRef = ref _world.Get<RigidbodyRef>(vector);
        ref var movementVector = ref _world.Get<MovementVector>(vector);
        var velocity = rigidbodyRef.Rigidbody.velocity;
        movementVector.Speed.y = Mathf.Abs(velocity.y);
        movementVector.Direction.y = Mathf.Sign(velocity.y);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadHeroViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _vectors;

    public ReadHeroViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _vectors = _world.Filter<RigidbodyRef>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        Vector2 velocity = vector.Get<RigidbodyRef>().Rigidbody.velocity;
        ref MovementVector movementVector = ref vector.Get<MovementVector>();
        
        movementVector.Speed = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));
        
        if (Mathf.Abs(velocity.x) > 0)
          movementVector.Direction.x = Mathf.Sign(velocity.x);
        movementVector.Direction.y = Mathf.Sign(velocity.y);
      }
    }
  }
}
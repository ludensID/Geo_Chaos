using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.View
{
  public class ReadViewVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _vectors;

    public ReadViewVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _vectors = _game
        .Filter<RigidbodyRef>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors
        .Where<MovementVector>(x => !x.Immutable))
      {
        Vector2 velocity = vector.Get<RigidbodyRef>().Rigidbody.velocity;
        (Vector3 length, Vector3 direction) = MathUtils.DecomposeVector(velocity);
        vector.Replace((ref MovementVector movementVector) =>
        {
          movementVector.Speed = length;
          if (length.x != 0)
            movementVector.Direction.x = direction.x;
          movementVector.Direction.y = direction.y;
        });
      }
    }
  }
}
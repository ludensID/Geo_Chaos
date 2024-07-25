using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
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
        .Check<MovementVector>(x => !x.Immutable))
      {
        Vector2 velocity = vector.Get<RigidbodyRef>().Rigidbody.velocity;
        vector.Change((ref MovementVector movementVector) => movementVector.AssignVector(velocity, true));
      }
    }
  }
}
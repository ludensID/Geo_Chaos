using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CalculateTargetVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsWorld _game;
    private readonly EcsEntities _owners;
    private readonly EcsEntities _simpleForces;
    private readonly EcsEntities _addedForces;
    private readonly EcsEntities _uniqueForces;

    public CalculateTargetVelocitySystem(PhysicsWorldWrapper physicsWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _owners = _game
        .Filter<ForceAvailable>()
        .Inc<MovementVector>()
        .Collect();

      _simpleForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Exc<Added>()
        .Exc<Unique>()
        .Collect();

      _addedForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Inc<Added>()
        .Collect();

      _uniqueForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Inc<Unique>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity owner in _owners)
      {
        ref MovementVector movementVector = ref owner.Get<MovementVector>();
        movementVector.Immutable = false;
        Vector2 velocity = movementVector.Speed * movementVector.Direction;

        foreach (EcsEntity force in _simpleForces
          .Where<Owner>(x => x.Entity.EqualsTo(owner.Pack())))
        {
          velocity = AssignVelocityByImpact(force, velocity);

          if (force.Is<Instant>())
          {
            force.Dispose();
          }
        }
        
        foreach (EcsEntity force in _addedForces
          .Where<Owner>(x => x.Entity.EqualsTo(owner.Pack())))
        {
          velocity = AddVelocityByImpact(force, velocity);
        }
        
        foreach (EcsEntity force in _uniqueForces
          .Where<Owner>(x => x.Entity.EqualsTo(owner.Pack())))
        {
          velocity = AssignVelocityByImpact(force, velocity);
          movementVector.Immutable = force.Is<Immutable>();
        }

        (Vector3 length, Vector3 direction) = MiscUtils.DecomposeVector(velocity);
        movementVector.Speed = length;
        if (movementVector.Speed.x != 0)
          movementVector.Direction.x = direction.x; 
        movementVector.Direction.y = direction.y;
      }
    }

    private Vector2 AssignVelocityByImpact(EcsEntity force, Vector2 velocity)
    {
      return ChangeVelocityByImpact(force, velocity, (v, s, d) => s * d);
    }

    private Vector2 AddVelocityByImpact(EcsEntity force, Vector2 velocity)
    {
      return ChangeVelocityByImpact(force, velocity, (v, s, d) => v + s * d);
    }

    private Vector2 ChangeVelocityByImpact(EcsEntity force, Vector2 velocity, Func<float, float, float, float> @operator)
    {
      ref Impact impact = ref force.Get<Impact>();
      ref MovementVector vector = ref force.Get<MovementVector>();
      for (int i = 0; i < 2; i++)
      {
        if (impact.Vector[i] > 0)
          velocity[i] = @operator.Invoke(velocity[i], vector.Speed[i], vector.Direction[i]);
      }

      return velocity;
    }
  }
}
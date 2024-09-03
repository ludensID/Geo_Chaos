using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CalculateTargetMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsWorld _game;
    private readonly EcsEntities _owners;
    private readonly EcsEntities _simpleForces;
    private readonly EcsEntities _addedForces;
    private readonly EcsEntities _uniqueForces;
    private readonly BelongOwnerClosure _belongOwnerClosure;
    private readonly EcsEntities _spareForces;

    public CalculateTargetMovementVectorSystem(PhysicsWorldWrapper physicsWorldWrapper,
      GameWorldWrapper gameWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _owners = _game
        .Filter<ForceAvailable>()
        .Inc<MovementVector>()
        .Collect();

      _belongOwnerClosure = new BelongOwnerClosure();

      _spareForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Inc<Spare>()
        .Exc<Added>()
        .Exc<Unique>()
        .Exc<Ignored>()
        .Collect();

      _simpleForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Exc<Added>()
        .Exc<Spare>()
        .Exc<Unique>()
        .Exc<Ignored>()
        .Collect();

      _addedForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Inc<Added>()
        .Exc<Ignored>()
        .Collect();

      _uniqueForces = _physics
        .Filter<MovementVector>()
        .Inc<Owner>()
        .Inc<Unique>()
        .Exc<Ignored>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity owner in _owners)
      {
        ref MovementVector movementVector = ref owner.Get<MovementVector>();
        movementVector.Immutable = false;
        Vector2 velocity = movementVector.Speed * movementVector.Direction;

        foreach (EcsEntity force in _spareForces
          .Check(_belongOwnerClosure.SpecifyPredicate(owner.PackedEntity)))
        {
          velocity = AssignVelocityByImpact(force, velocity);
        }
          
        foreach (EcsEntity force in _simpleForces
          .Check(_belongOwnerClosure.SpecifyPredicate(owner.PackedEntity)))
        {
          velocity = AssignVelocityByImpact(force, velocity);
        }

        foreach (EcsEntity force in _addedForces
          .Check(_belongOwnerClosure.SpecifyPredicate(owner.PackedEntity)))
        {
          velocity = AddVelocityByImpact(force, velocity);
        }

        foreach (EcsEntity force in _uniqueForces
          .Check(_belongOwnerClosure.SpecifyPredicate(owner.PackedEntity)))
        {
          velocity = AssignVelocityByImpact(force, velocity);
          movementVector.Immutable = force.Has<Immutable>();
        }

        velocity = ClampToZero(velocity);
        movementVector.AssignVector(velocity, true);
      }
    }

    private Vector2 AssignVelocityByImpact(EcsEntity force, Vector2 velocity)
    {
      return ChangeVelocityByImpact(force, velocity, (_, s, d) => s * d);
    }

    private Vector2 AddVelocityByImpact(EcsEntity force, Vector2 velocity)
    {
      return ChangeVelocityByImpact(force, velocity, (v, s, d) => v + s * d);
    }

    private Vector2 ChangeVelocityByImpact(EcsEntity force, Vector2 velocity,
      Func<float, float, float, float> vectorOperator)
    {
      ref Impact impact = ref force.Get<Impact>();
      ref MovementVector vector = ref force.Get<MovementVector>();
      for (int i = 0; i < 2; i++)
      {
        if (impact.Vector[i] > 0)
          velocity[i] = vectorOperator.Invoke(velocity[i], vector.Speed[i], vector.Direction[i]);
      }

      return velocity;
    }

    private static Vector2 ClampToZero(Vector2 vector)
    {
      vector.x = MathUtils.ClampTo(vector.x, 0);
      vector.y = MathUtils.ClampTo(vector.y, 0);

      return vector;
    }
}
}
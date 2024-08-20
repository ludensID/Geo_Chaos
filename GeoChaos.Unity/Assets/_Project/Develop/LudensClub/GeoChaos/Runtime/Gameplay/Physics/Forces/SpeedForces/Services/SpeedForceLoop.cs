using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceLoop
  {
    private readonly IsEntityOwnerClosure _isEntityOwnerClosure = new IsEntityOwnerClosure();
    private readonly EcsEntities _entities;

    public SpeedForceLoop(EcsEntities entities)
    {
      _entities = entities;
    }

    public EcsEntities GetEntities()
    {
      return _entities;
    }

    public EcsEntity GetForce(SpeedForceType type, EcsPackedEntity owner)
    {
      return GetLoop(type, owner).ToEnumerable().FirstOrDefault();
    }

    public EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner)
    {
      return _entities.Clone()
        .Where<SpeedForce>(x => x.Type == type)
        .Where<Owner>(x => x.Entity.EqualsTo(owner));
    }
    
    public void ResetForcesToZero(SpeedForceType type, EcsPackedEntity owner)
    {
      foreach (EcsEntity force in _entities
        .Check<SpeedForce>(x => x.Type == type)
        .Check(_isEntityOwnerClosure.SpecifyPredicate(owner)))
      {
        force
          .Change((ref MovementVector vector) => vector.Speed = Vector2.zero)
          .Add<Instant>();
      }
    }
  }
}
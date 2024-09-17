using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class SpeedForceLoop
  {
    private readonly BelongOwnerClosure _belongOwnerClosure = new BelongOwnerClosure();
    private readonly EcsEntities _entities;

    public SpeedForceLoop(EcsEntities entities)
    {
      _entities = entities;
    }

    public EcsEntities GetLoop(SpeedForceType type, EcsPackedEntity owner)
    {
      return _entities.Clone()
        .Where<SpeedForce>(x => x.Type == type)
        .Where(_belongOwnerClosure.SpecifyPredicate(owner));
    }
    
    public void ResetForcesToZero(SpeedForceType type, EcsPackedEntity owner)
    {
      foreach (EcsEntity force in _entities
        .Check<SpeedForce>(x => x.Type == type)
        .Check(_belongOwnerClosure.SpecifyPredicate(owner)))
      {
        force
          .Change((ref MovementVector vector) => vector.Speed = Vector2.zero)
          .Add<Instant>()
          .Add<Spare>();
      }
    }
  }
}
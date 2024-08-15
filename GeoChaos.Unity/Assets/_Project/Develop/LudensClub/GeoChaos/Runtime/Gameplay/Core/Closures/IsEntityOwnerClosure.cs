using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class IsEntityOwnerClosure : EcsClosure<Owner>
  {
    public EcsPackedEntity PackedEntity;

    public Predicate<Owner> SpecifyPredicate(EcsPackedEntity packedEntity)
    {
      PackedEntity = packedEntity;
      return Predicate;
    }

    protected override bool Call(Owner owner)
    {
      return owner.Entity.EqualsTo(PackedEntity);
    }
  }
}
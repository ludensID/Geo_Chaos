using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class BelongOwnerClosure : SpecifiedInternalClosure<Owner, EcsPackedEntity>
  {
    protected override bool Call(Owner value, EcsPackedEntity data)
    {
      return value.Entity.EqualsTo(data);
    }
  }
}
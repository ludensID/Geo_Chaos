using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct TwoSideCollision : IEcsComponent
  {
    public PackedCollider Sender;
    public PackedCollider Other;

    public TwoSideCollision(PackedCollider sender, PackedCollider other)
    {
      Sender = sender;
      Other = other;
    }
  }
}
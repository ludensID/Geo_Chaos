using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct TwoSideCollision : IEcsComponent
  {
    public CollisionType Type;
    public PackedCollider Sender;
    public PackedCollider Other;

    public TwoSideCollision(CollisionType type, PackedCollider sender, PackedCollider other)
    {
      Type = type;
      Sender = sender;
      Other = other;
    }
  }
}
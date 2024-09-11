using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  [EcsComponentOrder(EcsComponentOrder.TEMPORARY)]
  public struct OneSideCollision : IEcsComponent
  {
    public CollisionType Type;
    public PackedCollider Sender;
    public Collider2D Other;

    public OneSideCollision(CollisionType type, PackedCollider sender, Collider2D other)
    {
      Type = type;
      Sender = sender;
      Other = other;
    }
  }
}
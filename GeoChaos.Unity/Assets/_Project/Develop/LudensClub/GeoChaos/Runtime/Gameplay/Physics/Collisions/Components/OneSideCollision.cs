using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
#if ENABLE_IL2CPP
  using Unity.IL2CPP.CompilerServices;
    
  [Il2CppSetOption(Option.NullChecks, false)]
  [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
  [Serializable]
  public struct OneSideCollision : IEcsComponent
  {
    public PackedCollider Sender;
    public Collider2D Other;

    public OneSideCollision(PackedCollider sender, Collider2D other)
    {
      Sender = sender;
      Other = other;
    }
  }
}
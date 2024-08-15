using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionFiller
  {
    void Fill(CollisionType type, PackedCollider packedCollider, Collider2D other);
    List<OneSideCollision> Flush();
    List<OneSideCollision> FlushFixed();
  }
}
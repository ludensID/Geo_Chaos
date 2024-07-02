using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionFiller
  {
    void Fill(Collider2D sender, ColliderType senderType, EcsPackedEntity entity, Collider2D other);
    List<OneSideCollision> Flush();
  }
}
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionFiller : ICollisionFiller
  {
    private readonly List<OneSideCollision> _collisions = new List<OneSideCollision>();

    public void Fill(CollisionType type, Collider2D sender, ColliderType senderType, EcsPackedEntity entity, Collider2D other)
    {
      _collisions.Add(new OneSideCollision(type, new PackedCollider(sender, senderType, entity), other));
    }

    public List<OneSideCollision> Flush()
    {
      var collisions = new List<OneSideCollision>(_collisions);
      _collisions.Clear();
      return collisions;
    }
  }
}
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionFiller : ICollisionFiller
  {
    private readonly List<OneSideCollision> _fixedCollisions = new List<OneSideCollision>();
    private readonly List<OneSideCollision> _collisions = new List<OneSideCollision>();

    public void Fill(CollisionType type, Collider2D sender, ColliderType senderType, EcsPackedEntity entity, Collider2D other)
    {
      var collision = new OneSideCollision(type, new PackedCollider(sender, senderType, entity), other);
      _fixedCollisions.Add(collision);
      _collisions.Add(collision);
    }

    public List<OneSideCollision> Flush()
    {
      var collisions = new List<OneSideCollision>(_collisions);
      _collisions.Clear();
      return collisions;
    }

    public List<OneSideCollision> FlushFixed()
    {
      var collisions = new List<OneSideCollision>(_fixedCollisions);
      _fixedCollisions.Clear();
      return collisions;
    }
  }
}
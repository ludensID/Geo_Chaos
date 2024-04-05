using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public class CollisionFiller : ICollisionFiller
  {
    private readonly List<OneSideCollision> _list = new List<OneSideCollision>();

    public void Fill(Collider2D sender, ColliderType senderType, EcsPackedEntity entity, Collider2D other)
    {
      _list.Add(new OneSideCollision(new PackedCollider(sender, senderType, entity), other));
    }

    public List<TwoSideCollision> Flush()
    {
      var selection = new List<OneSideCollision>(_list);
      var collisions = new List<TwoSideCollision>();
      foreach (OneSideCollision collision in _list)
      {
        var other = selection.Find(x => x.Sender.Collider == collision.Other);
        if (other.Other == null)
        {
          selection.Remove(collision);
          continue;
        }

        collisions.Add(new TwoSideCollision(collision.Sender, other.Sender));
        selection.Remove(collision);
        selection.Remove(other);
      }

      _list.Clear();
      return collisions;
    }
  }
}
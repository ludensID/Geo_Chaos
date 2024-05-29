using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  [Serializable]
  public struct PackedCollider
  {
    public Collider2D Collider;
    public ColliderType Type;
    public EcsPackedEntity Entity;

    public PackedCollider(Collider2D collider, ColliderType type, EcsPackedEntity entity)
    {
      Collider = collider;
      Type = type;
      Entity = entity;
    }
  }
}
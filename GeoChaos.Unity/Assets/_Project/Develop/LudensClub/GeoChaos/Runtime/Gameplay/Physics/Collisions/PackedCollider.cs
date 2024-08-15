using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  [Serializable]
  public struct PackedCollider
  {
    public Collider2D Collider;
    public Vector3 EntityPosition;
    public ColliderType Type;
    public EcsPackedEntity Entity;

    public PackedCollider(Collider2D collider, Vector3 entityPosition, ColliderType type, EcsPackedEntity entity)
    {
      Collider = collider;
      EntityPosition = entityPosition;
      Type = type;
      Entity = entity;
    }
  }
}
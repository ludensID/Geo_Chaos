using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props
{
  public interface ICollisionFiller
  {
    void Fill(Collider2D sender, ColliderType senderType, EcsPackedEntity entity, Collider2D other);
    List<OneSideCollision> Flush();
  }
}
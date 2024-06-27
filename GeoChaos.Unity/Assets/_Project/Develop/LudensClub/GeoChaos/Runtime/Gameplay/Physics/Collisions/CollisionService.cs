using System.Collections.Generic;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionService : ICollisionService
  {
    public bool TrySelectDamagerAndTargetColliders(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out PackedCollider damager, out PackedCollider target)
    {
      var selection = new List<PackedCollider> { collision.Sender, collision.Other };
      damager = selection.Find(x => x.Type == damagerType);
      target = selection.Find(x => x.Type == targetType);

      return damager.Collider && target.Collider;
    }
    
    public bool TrySelectDamagerAndTargetEntities(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out EcsPackedEntity damager, out EcsPackedEntity target)
    {
      var selection = new List<PackedCollider> { collision.Sender, collision.Other };
      PackedCollider damagerCollider = selection.Find(x => x.Type == damagerType);
      PackedCollider targetCollider = selection.Find(x => x.Type == targetType);
      
      damager = damagerCollider.Entity;
      target = targetCollider.Entity;
    
      return damagerCollider.Collider && targetCollider.Collider;
    }
  }
}
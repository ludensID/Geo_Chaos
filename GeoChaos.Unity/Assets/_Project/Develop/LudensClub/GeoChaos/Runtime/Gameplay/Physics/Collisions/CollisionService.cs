using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionService : ICollisionService
  {
    public bool TrySelectDamagerAndTarget(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out PackedCollider damager, out PackedCollider target)
    {
      var selection = new List<PackedCollider> { collision.Sender, collision.Other };
      damager = selection.Find(x => x.Type == damagerType);
      target = selection.Find(x => x.Type == targetType);

      return damager.Collider && target.Collider;
    }
  }
}
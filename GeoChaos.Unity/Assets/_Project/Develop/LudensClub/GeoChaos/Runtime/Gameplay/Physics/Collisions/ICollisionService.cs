using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionService
  {
    bool TrySelectDamagerAndTargetColliders(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out PackedCollider damager, out PackedCollider target);

    bool TrySelectDamagerAndTargetEntities(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out EcsPackedEntity damager, out EcsPackedEntity target);
  }
}
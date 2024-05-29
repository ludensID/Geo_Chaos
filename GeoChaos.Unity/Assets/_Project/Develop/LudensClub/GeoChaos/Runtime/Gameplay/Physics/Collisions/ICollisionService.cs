using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionService
  {
    bool TrySelectDamagerAndTarget(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out PackedCollider damager, out PackedCollider target);
  }
}
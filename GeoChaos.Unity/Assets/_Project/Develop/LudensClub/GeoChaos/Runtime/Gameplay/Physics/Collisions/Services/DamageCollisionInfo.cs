using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageCollisionInfo
  {
    public PackedCollider MasterCollider;
    public PackedCollider TargetCollider;

    public EcsPackedEntity PackedMaster => MasterCollider.Entity;
    public EcsPackedEntity PackedTarget => TargetCollider.Entity;

    public EcsEntity Master;
    public EcsEntity Target;

    public void Reset()
    {
      MasterCollider = new PackedCollider();
      TargetCollider = new PackedCollider();
      Master = null;
      Target = null;
    }
  }
}
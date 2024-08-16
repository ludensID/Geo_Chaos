using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionService
  {
    CollisionInfo Info { get; }
    void AssignCollision(TwoSideCollision collision);
    void AssignCollision(OneSideCollision collision);
    bool TrySelectByMasterCollider(Predicate<PackedCollider> selector, bool sync = true);
    bool TrySelectByTargetCollider(Predicate<PackedCollider> selector, bool sync = true);

    bool TrySelectByColliders(Predicate<PackedCollider> masterSelector, Predicate<PackedCollider> targetSelector,
      bool sync = true);

    bool TrySelectByMasterEntity(Predicate<EcsEntity> selector, bool sync = true);
    bool TrySelectByTargetEntity(Predicate<EcsEntity> selector, bool sync = true);
    bool TrySelectByEntities(Predicate<EcsEntity> masterSelector, Predicate<EcsEntity> targetSelector, bool sync = true);
    bool SyncCollidersWithEntities();
    bool SyncEntitiesWithColliders();
    bool TryUnpackBothEntities(EcsWorld world);
    bool UnpackEntities(EcsWorld world);
    bool TryUnpackByMasterEntity(EcsWorld world);
    bool TryUnpackByTargetEntity(EcsWorld world);
    bool TryUnpackAnyEntity(EcsWorld world);
  }
}
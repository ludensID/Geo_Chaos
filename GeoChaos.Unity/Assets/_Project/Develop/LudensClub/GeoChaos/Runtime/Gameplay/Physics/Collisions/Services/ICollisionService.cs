using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public interface ICollisionService
  {
    DamageCollisionInfo Info { get; }
    void AssignCollision(TwoSideCollision collision);
    void AssignCollision(OneSideCollision collision);
    bool TrySelectByMasterCollider(Predicate<PackedCollider> selector, bool sync = false);
    bool TrySelectByTargetCollider(Predicate<PackedCollider> selector, bool sync = false);

    bool TrySelectByColliders(Predicate<PackedCollider> masterSelector, Predicate<PackedCollider> targetSelector,
      bool sync = false);

    bool TrySelectByMasterEntity(Predicate<EcsEntity> selector, bool sync = false);
    bool TrySelectByTargetEntity(Predicate<EcsEntity> selector, bool sync = false);
    bool TrySelectByEntities(Predicate<EcsEntity> masterSelector, Predicate<EcsEntity> targetSelector, bool sync = false);
    bool SyncCollidersWithEntities();
    bool SyncEntitiesWithColliders();
    bool TryUnpackEntities(EcsWorld world);
    void Reset();
  }
}
using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class CollisionService : ICollisionService
  {
    public DamageCollisionInfo Info { get; } = new DamageCollisionInfo();

    public void AssignCollision(TwoSideCollision collision)
    {
      Reset();
      Info.MasterCollider = collision.Sender;
      Info.TargetCollider = collision.Other;
    }

    public void AssignCollision(OneSideCollision collision)
    {
      Reset();
      Info.MasterCollider = collision.Sender;
      Info.TargetCollider.Collider = collision.Other;
    }

    public bool TrySelectByMasterCollider(Predicate<PackedCollider> selector, bool sync = true)
    {
      var selection = new List<PackedCollider> { Info.MasterCollider, Info.TargetCollider };
      return TrySelect(() => SelectByMaster(() => selection.FindIndex(selector)),
        AssignColliders(selection),
        sync, () => SyncEntitiesWithColliders());
    }

    public bool TrySelectByTargetCollider(Predicate<PackedCollider> selector, bool sync = true)
    {
      var selection = new List<PackedCollider> { Info.MasterCollider, Info.TargetCollider };
      return TrySelect(() => SelectByTarget(() => selection.FindIndex(selector)),
        AssignColliders(selection),
        sync, () => SyncEntitiesWithColliders());
    }

    public bool TrySelectByColliders(Predicate<PackedCollider> masterSelector, Predicate<PackedCollider> targetSelector,
      bool sync = true)
    {
      var selection = new List<PackedCollider> { Info.MasterCollider, Info.TargetCollider };
      return TrySelect(
        () => SelectBoth(() => selection.FindIndex(masterSelector), () => selection.FindIndex(targetSelector)),
        AssignColliders(selection),
        sync, () => SyncEntitiesWithColliders());
    }

    public bool TrySelectByMasterEntity(Predicate<EcsEntity> selector, bool sync = true)
    {
      var selection = new List<EcsEntity> { Info.Master, Info.Target };
      return TrySelect(() => SelectByMaster(() => selection.FindIndex(selector)),
        AssignEntities(selection),
        sync, () => SyncCollidersWithEntities());
    }

    public bool TrySelectByTargetEntity(Predicate<EcsEntity> selector, bool sync = true)
    {
      var selection = new List<EcsEntity> { Info.Master, Info.Target };
      return TrySelect(() => SelectByTarget(() => selection.FindIndex(selector)),
        AssignEntities(selection),
        sync, () => SyncCollidersWithEntities());
    }

    public bool TrySelectByEntities(Predicate<EcsEntity> masterSelector, Predicate<EcsEntity> targetSelector,
      bool sync = true)
    {
      var selection = new List<EcsEntity> { Info.Master, Info.Target };
      return TrySelect(
        () => SelectBoth(() => selection.FindIndex(masterSelector), () => selection.FindIndex(targetSelector)),
        AssignEntities(selection),
        sync, () => SyncCollidersWithEntities());
    }

    public bool SyncCollidersWithEntities()
    {
      return TrySelectByColliders(x => x.Entity.EqualsTo(Info.Master.Pack()),
        x => x.Entity.EqualsTo(Info.Target.Pack()), false);
    }

    public bool SyncEntitiesWithColliders()
    {
      return TrySelectByEntities(x => Info.MasterCollider.Entity.EqualsTo(x.Pack()),
        x => Info.TargetCollider.Entity.EqualsTo(x.Pack()), false);
    }

    public bool TryUnpackEntities(EcsWorld world)
    {
      return Info.MasterCollider.Entity.TryUnpackEntity(world, out Info.Master)
        && Info.TargetCollider.Entity.TryUnpackEntity(world, out Info.Target);
    }

    public bool UnpackEntities(EcsWorld world)
    {
      Info.MasterCollider.Entity.TryUnpackEntity(world, out Info.Master);
      Info.TargetCollider.Entity.TryUnpackEntity(world, out Info.Target);
      return true;
    }

    public void Reset()
    {
      Info.Reset();
    }

    private Action<int, int> AssignColliders(List<PackedCollider> selection)
    {
      return (master, target) =>
      {
        Info.MasterCollider = selection[master];
        Info.TargetCollider = selection[target];
      };
    }

    private Action<int, int> AssignEntities(List<EcsEntity> selection)
    {
      return (master, target) =>
      {
        Info.Master = selection[master];
        Info.Target = selection[target];
      };
    }

    private (int, int) SelectByMaster(Func<int> selector)
    {
      int masterIndex = selector();
      int targetIndex = (masterIndex + 1) % 2;

      return (masterIndex, targetIndex);
    }

    private (int, int) SelectByTarget(Func<int> selector)
    {
      int targetIndex = selector();
      int masterIndex = (targetIndex + 1) % 2;

      return (masterIndex, targetIndex);
    }

    private (int, int) SelectBoth(Func<int> masterSelector, Func<int> targetSelector)
    {
      int masterIndex = masterSelector();
      int targetIndex = targetSelector();

      return (masterIndex, targetIndex);
    }

    private bool TrySelect(Func<(int master, int target)> selector, Action<int, int> assigner, bool sync, Action syncer)
    {
      return TrySelectInternal(selector, assigner, sync ? syncer : null);
    }

    private bool TrySelectInternal(Func<(int master, int target)> selector, Action<int, int> assigner,
      Action syncer = null)
    {
      (int master, int target) = selector();
      if (!TryAssign(assigner, master, target))
        return false;

      syncer?.Invoke();
      return true;
    }


    private static bool TryAssign(Action<int, int> assigner, int masterIndex, int targetIndex)
    {
      if (masterIndex == -1 || targetIndex == -1 || masterIndex == targetIndex)
        return false;

      assigner(masterIndex, targetIndex);

      return true;
    }
  }
}
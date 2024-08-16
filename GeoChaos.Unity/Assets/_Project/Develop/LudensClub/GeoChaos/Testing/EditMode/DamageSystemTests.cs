using FluentAssertions;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NUnit.Framework;
using UnityEngine;

namespace LudensClub.GeoChaos.Testing.EditMode
{
  public class DamageSystemTests
  {
    [Test]
    public void
      WhenTrySelectByMasterColliderAndColliderTypeIsShardAndTargetColliderExistsThenMasterEntityShouldBeShard()
    {
      // Arrange.
      EcsWorld world = new EcsWorld();
      EcsEntity master = world.CreateEntity().Add((ref EntityId id) => id.Id = EntityType.Shard);
      EcsEntity target = world.CreateEntity().Add((ref EntityId id) => id.Id = EntityType.Enemy);
      CollisionService collisionSvc = new CollisionService();
      CollisionInfo info = collisionSvc.Info;
      var sender = new PackedCollider(null, Vector3.zero,  ColliderType.Attack, master.PackedEntity);
      var other = new PackedCollider(null, Vector3.zero, ColliderType.Body, target.PackedEntity);
      collisionSvc.AssignCollision(new TwoSideCollision(CollisionType.Enter, other, sender));
      collisionSvc.TryUnpackBothEntities(world);

      // Act.
      collisionSvc.TrySelectByMasterCollider(x => x.Type == ColliderType.Attack);

      // Assert.
      info.Master.Get<EntityId>().Id.Should().Be(EntityType.Shard);
    }
  }
}
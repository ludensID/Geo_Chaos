using FluentAssertions;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using NUnit.Framework;

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
      DamageCollisionInfo info = collisionSvc.Info;
      var sender = new PackedCollider(null, ColliderType.Attack, master.Pack());
      var other = new PackedCollider(null, ColliderType.Body, target.Pack());
      collisionSvc.AssignCollision(new TwoSideCollision(CollisionType.Enter, other, sender));
      collisionSvc.TryUnpackEntities(world);

      // Act.
      collisionSvc.TrySelectByMasterCollider(x => x.Type == ColliderType.Attack);

      // Assert.
      info.Master.Get<EntityId>().Id.Should().Be(EntityType.Shard);
    }
  }
}
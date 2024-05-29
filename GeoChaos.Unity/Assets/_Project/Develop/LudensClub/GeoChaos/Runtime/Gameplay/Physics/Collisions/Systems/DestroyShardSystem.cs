using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DestroyShardSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _oneCollisions;
    private readonly EcsEntities _twoCollisions;

    public DestroyShardSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _oneCollisions = _message
        .Filter<OneSideCollision>()
        .Collect();

      _twoCollisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity collision in _oneCollisions)
      {
        DestroyShard(collision.Get<OneSideCollision>().Sender);
      }

      foreach (EcsEntity collision in _twoCollisions)
      {
        ref TwoSideCollision sideCollision = ref collision.Get<TwoSideCollision>();
        DestroyShard(sideCollision.Sender, sideCollision.Other);
        DestroyShard(sideCollision.Other, sideCollision.Sender);
      }
    }

    private void DestroyShard(PackedCollider collider, PackedCollider other = new PackedCollider())
    {
      if (collider.Type == ColliderType.Shard && collider.Entity.TryUnpackEntity(_game, out EcsEntity shard)
        && (other.Collider == null || !shard.Get<Owner>().Entity.EqualsTo(other.Entity)))
        shard.Has<DestroyCommand>(true);
    }
  }
}
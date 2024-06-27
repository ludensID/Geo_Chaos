using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageFromShardSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsEntities _twoCollisions;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _oneCollisions;

    public DamageFromShardSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _twoCollisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
      
      _oneCollisions = _message
        .Filter<OneSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _twoCollisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        if (DestroyShard(collision.Sender, collision.Other) || DestroyShard(collision.Other, collision.Sender))
        {
          if (_collisionSvc.TrySelectDamagerAndTargetColliders(collision, ColliderType.Shard, ColliderType.Body,
              out PackedCollider damager, out PackedCollider target)
            && damager.Entity.TryUnpackEntity(_game, out EcsEntity shard)
            && !shard.Get<Owner>().Entity.EqualsTo(target.Entity))
          {
            _message.CreateEntity()
              .Add((ref DamageMessage damage) =>
              {
                damage.Damage = _config.ShardDamage;
                damage.Damager = damager.Entity;
                damage.Target = target.Entity;
              });
          }
        }
      }
      
      foreach (EcsEntity collision in _oneCollisions)
      {
        DestroyShard(collision.Get<OneSideCollision>().Sender);
      }
    }
    
    private bool DestroyShard(PackedCollider collider, PackedCollider other = new PackedCollider())
    {
      if (collider.Type == ColliderType.Shard && collider.Entity.TryUnpackEntity(_game, out EcsEntity shard)
        && (other.Collider == null || !shard.Get<Owner>().Entity.EqualsTo(other.Entity)))
      {
        shard.Has<DestroyCommand>(true);
        return true;
      }

      return false;
    }
  }
}
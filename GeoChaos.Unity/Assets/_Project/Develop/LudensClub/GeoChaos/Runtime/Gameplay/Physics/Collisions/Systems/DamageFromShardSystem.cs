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
        _collisionSvc.AssignCollision(col.Get<TwoSideCollision>());
        DamageCollisionInfo info = _collisionSvc.Info;
        if (DestroyShard(info) && info.TargetCollider.Type == ColliderType.Body)
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) =>
            {
              damage.Damage = _config.ShardDamage;
              damage.Master = info.PackedMaster;
              damage.Target = info.PackedTarget;
            });
        }
        
        _collisionSvc.Reset();
      }

      foreach (EcsEntity collision in _oneCollisions)
      {
        _collisionSvc.AssignCollision(collision.Get<OneSideCollision>());
        DestroyShard(_collisionSvc.Info);
        _collisionSvc.Reset();
      }
    }

    private bool DestroyShard(DamageCollisionInfo info)
    {
      if (_collisionSvc.TryUnpackEntities(_game)
        && _collisionSvc.TrySelectByMasterCollider(x => x.Type == ColliderType.Shard)
        && info.TargetCollider.Type != ColliderType.Action
        && (!info.Target.IsAlive || !info.Master.Get<Owner>().Entity.EqualsTo(info.PackedTarget)))
      {
        info.Master.Has<DestroyCommand>(true);
        return true;
      }

      return false;
    }
  }
}
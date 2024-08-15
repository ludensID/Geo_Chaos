using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class DamageFromShardSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsEntities _twoCollisions;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _oneCollisions;

    public DamageFromShardSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
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
        if (DestroyShard(info)
          && info.TargetCollider.Type == ColliderType.Body
          && info.Target.IsAlive()
          && info.Target.Has<Damageable>())
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) => damage.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.ShardDamage, info.MasterCollider.EntityPosition));
        }
      }

      foreach (EcsEntity collision in _oneCollisions)
      {
        _collisionSvc.AssignCollision(collision.Get<OneSideCollision>());
        DestroyShard(_collisionSvc.Info);
      }
    }

    private bool DestroyShard(DamageCollisionInfo info)
    {
      if (_collisionSvc.UnpackEntities(_game)
        && _collisionSvc.TrySelectByMasterCollider(x => x.Type == ColliderType.Attack)
        && info.Master.IsAlive()
        && info.Master.Has<ShardTag>()
        && info.TargetCollider.Type != ColliderType.Action
        && (!info.Target.IsAlive() || !info.Master.Get<Owner>().Entity.EqualsTo(info.PackedTarget)))
      {
        info.Master.SetActive(false);
        info.Master.Has<DestroyCommand>(true);
        return true;
      }

      return false;
    }
  }
}
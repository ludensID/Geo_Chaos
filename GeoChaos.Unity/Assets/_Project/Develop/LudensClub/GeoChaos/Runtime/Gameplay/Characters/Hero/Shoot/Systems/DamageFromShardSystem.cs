using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class DamageFromShardSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsEntities _twoCollisions;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;

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
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _twoCollisions)
      {
        _collisionSvc.AssignCollision(col.Get<TwoSideCollision>());
        CollisionInfo info = _collisionSvc.Info;
        if (_collisionSvc.TryUnpackBothEntities(_game)
          && _collisionSvc.TrySelectByMasterEntity(x => x.Has<ShardTag>())
          && info.TargetCollider.Type == ColliderType.Body
          && info.Target.Has<Damageable>()
          && !info.Master.Get<Owner>().Entity.EqualsTo(info.PackedTarget))
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) => damage.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.ShardDamage, info.MasterCollider.EntityPosition));
        }
      }
    }
  }
}
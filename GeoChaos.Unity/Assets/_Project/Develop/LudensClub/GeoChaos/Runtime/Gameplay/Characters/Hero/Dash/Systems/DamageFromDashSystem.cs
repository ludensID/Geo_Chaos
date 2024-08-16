using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash
{
  public class DamageFromDashSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsEntities _collisions;
    private readonly EcsWorld _game;

    public DamageFromDashSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _collisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        CollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackBothEntities(_game)
          && _collisionSvc.TrySelectByColliderTypes(ColliderType.Dash, ColliderType.Body)
          && !info.PackedMaster.EqualsTo(info.PackedTarget)
          && info.Target.Has<Damageable>())
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) => damage.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.DashDamage, info.MasterCollider.EntityPosition));
        }
      }
    }
  }
}
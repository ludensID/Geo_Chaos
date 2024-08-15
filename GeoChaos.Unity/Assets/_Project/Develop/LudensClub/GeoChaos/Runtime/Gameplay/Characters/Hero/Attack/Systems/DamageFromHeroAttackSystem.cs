using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Attack
{
  public class DamageFromHeroAttackSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;

    public DamageFromHeroAttackSystem(MessageWorldWrapper messageWorldWrapper,
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
        DamageCollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackBothEntities(_game)
          && _collisionSvc.TrySelectByMasterEntity(x => x.Has<HeroTag>())
          && info.MasterCollider.Type == ColliderType.Attack
          && info.TargetCollider.Type == ColliderType.Body
          && info.Target.Has<Damageable>()
          && !info.PackedMaster.EqualsTo(info.PackedTarget))
        {
          _message.CreateEntity()
            .Add((ref DamageMessage message) => message.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.HitDamages[info.Master.Get<ComboAttackCounter>().Count], info.MasterCollider.EntityPosition));
        }
      }
    }
  }
}
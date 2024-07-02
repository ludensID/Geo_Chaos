using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Attack
{
  public class DamageFromLamaAttackSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;
    private readonly LamaConfig _config;

    public DamageFromLamaAttackSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _collisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        _collisionSvc.AssignCollision(collision);
        DamageCollisionInfo info = _collisionSvc.Info;
        if (_collisionSvc.TrySelectByColliderTypes(ColliderType.Attack, ColliderType.Body)
          && _collisionSvc.TryUnpackEntities(_game)
          && !info.PackedMaster.EqualsTo(info.PackedTarget)
          && info.Master.Has<LamaTag>() && info.Target.Has<HeroTag>())
        {
          float damage = info.Master.Get<ComboAttackCounter>().Count == 3 ? _config.BiteDamage : _config.HitDamage;
          _message.CreateEntity()
            .Add((ref DamageMessage message) =>
            {
              message.Damage = damage;
              message.Damager = info.PackedMaster;
              message.Target = info.PackedTarget;
            });
        }
      }
    }
  }
}
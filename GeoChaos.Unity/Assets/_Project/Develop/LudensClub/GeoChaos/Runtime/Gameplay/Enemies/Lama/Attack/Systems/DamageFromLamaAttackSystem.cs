using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;

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
        if (_collisionSvc.TrySelectDamagerAndTargetEntities(collision, ColliderType.Attack, ColliderType.Body,
          out EcsPackedEntity packedDamager, out EcsPackedEntity packedTarget) && !packedDamager.EqualsTo(packedTarget))
        {
          if (packedDamager.TryUnpackEntity(_game, out EcsEntity damager)
            && packedTarget.TryUnpackEntity(_game, out EcsEntity target)
            && damager.Has<LamaTag>() && target.Has<HeroTag>())
          {
            float damage = damager.Get<ComboAttackCounter>().Count == 3 ? _config.BiteDamage : _config.HitDamage;
            _message.CreateEntity()
              .Add((ref DamageMessage message) =>
              {
                message.Damage = damage;
                message.Damager = packedDamager;
                message.Target = packedTarget;
              });
          }
        }
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
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
        if (_collisionSvc.TrySelectDamagerAndTargetColliders(collision, ColliderType.Attack, ColliderType.Body,
          out PackedCollider damager, out PackedCollider target) && !damager.Entity.EqualsTo(target.Entity))
        {
          if (damager.Entity.TryUnpackEntity(_game, out EcsEntity damagerEntity) && damagerEntity.Has<HeroTag>())
          {
            _message.CreateEntity()
              .Add((ref DamageMessage message) =>
              {
                message.Damage = _config.HitDamages[damagerEntity.Get<ComboAttackCounter>().Count];
                message.Damager = damager.Entity;
                message.Target = target.Entity;
              });
          }
        }
      }
    }
  }
}
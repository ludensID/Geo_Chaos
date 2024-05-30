using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageFromAttackSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;

    public DamageFromAttackSystem(MessageWorldWrapper messageWorldWrapper,
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
        if (_collisionSvc.TrySelectDamagerAndTarget(collision, ColliderType.Attack, ColliderType.Body,
          out PackedCollider damager, out PackedCollider target) && !damager.Entity.EqualsTo(target.Entity))
        {
          if (damager.Entity.TryUnpackEntity(_game, out EcsEntity damagerEntity))
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
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
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

    public DamageFromLamaAttackSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

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
            var ctx = damager.Get<BrainContext>().Cast<LamaContext>();
            float damage = damager.Get<ComboAttackCounter>().Count == 3 ? ctx.BiteDamage : ctx.HitDamage;
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
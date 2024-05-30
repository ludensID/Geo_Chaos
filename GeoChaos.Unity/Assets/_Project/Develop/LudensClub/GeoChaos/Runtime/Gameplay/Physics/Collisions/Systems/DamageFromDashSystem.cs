using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageFromDashSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsEntities _collisions;

    public DamageFromDashSystem(MessageWorldWrapper messageWorldWrapper,
      IConfigProvider configProvider,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
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
        if (_collisionSvc.TrySelectDamagerAndTarget(collision, ColliderType.Dash, ColliderType.Body,
          out PackedCollider damager, out PackedCollider target) && !damager.Entity.EqualsTo(target.Entity))
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) =>
            {
              damage.Damage = _config.DashDamage;
              damage.Damager = damager.Entity;
              damage.Target = target.Entity;
            });
        }
      }
    }
  }
}
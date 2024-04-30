using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Systems
{
  public class DamageFromDashSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsFilter _collisions;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;

    public DamageFromDashSystem(MessageWorldWrapper messageWorldWrapper,
      IConfigProvider configProvider,
      ICollisionService collisionSvc)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _collisions = _message
        .Filter<TwoSideCollision>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var col in _collisions)
      {
        ref TwoSideCollision collision = ref _message.Get<TwoSideCollision>(col);
        if (_collisionSvc.TrySelectDamagerAndTarget(collision, ColliderType.Dash, ColliderType.Body,
          out PackedCollider damager, out PackedCollider target) && !damager.Entity.EqualsTo(target.Entity))
        {
          int message = _message.NewEntity();
          ref DamageMessage damage = ref _message.Add<DamageMessage>(message);
          damage.Damage = _config.DashDamage;
          damage.Damager = damager.Entity;
          damage.Target = target.Entity;
        }
      }
    }
  }
}
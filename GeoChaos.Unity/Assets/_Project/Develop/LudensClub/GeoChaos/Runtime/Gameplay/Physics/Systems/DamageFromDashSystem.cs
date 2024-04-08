using System.Collections.Generic;
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
    private readonly EcsFilter _collisions;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;

    public DamageFromDashSystem(MessageWorldWrapper messageWorldWrapper,
      IConfigProvider configProvider)
    {
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
        ref var collision = ref _message.Get<TwoSideCollision>(col);
        if (TrySelect(collision, ColliderType.Dash, ColliderType.Body, out var damager,
          out var target) && !damager.Entity.EqualsTo(target.Entity))
        {
          var message = _message.NewEntity();
          ref var damage = ref _message.Add<DamageMessage>(message);
          damage.Damage = _config.DashDamage;
          damage.Damager = damager.Entity;
          damage.Target = target.Entity;
        }
      }
    }

    public bool TrySelect(TwoSideCollision collision, ColliderType damagerType, ColliderType targetType,
      out PackedCollider damager, out PackedCollider target)
    {
      var selection = new List<PackedCollider> { collision.Sender, collision.Other };
      damager = selection.Find(x => x.Type == damagerType);
      target = selection.Find(x => x.Type == targetType);

      return damager.Collider && target.Collider;
    }

    public bool DoubleOr(ColliderType aLeft, ColliderType bLeft, ColliderType aRight, ColliderType bRight)
    {
      return (aLeft == aRight && bLeft == bRight) || (aLeft == bRight && bLeft == aRight);
    }
  }
}
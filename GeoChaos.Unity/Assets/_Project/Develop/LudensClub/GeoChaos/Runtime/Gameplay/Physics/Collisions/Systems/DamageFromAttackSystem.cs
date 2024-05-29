using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageFromAttackSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsFilter _collisions;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsWorld _game;

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
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int col in _collisions)
      {
        ref TwoSideCollision collision = ref _message.Get<TwoSideCollision>(col);
        if (_collisionSvc.TrySelectDamagerAndTarget(collision, ColliderType.Attack, ColliderType.Body,
          out PackedCollider damager, out PackedCollider target) && !damager.Entity.EqualsTo(target.Entity))
        {
          if (damager.Entity.Unpack(_game, out int damagerEntity))
          {
            ref ComboAttackCounter counter = ref _game.Get<ComboAttackCounter>(damagerEntity);
            
            int message = _message.NewEntity();
            ref DamageMessage damage = ref _message.Add<DamageMessage>(message);
            damage.Damage = _config.HitDamages[counter.Count];
            damage.Damager = damager.Entity;
            damage.Target = target.Entity;
            Debug.Log($"Take Damage {damage.Damage}");
          }
        }
      }
    }
  }
}
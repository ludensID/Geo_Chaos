using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment
{
  public class DamageFromSpikeSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;
    private readonly SpikeConfig _config;

    public DamageFromSpikeSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<SpikeConfig>();

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
            && damager.Has<SpikeTag>() && target.Has<HeroTag>())
          {
            _message.CreateEntity()
              .Add((ref DamageMessage message) =>
              {
                message.Damage = _config.Damage;
                message.Damager = packedDamager;
                message.Target = packedTarget;
              });
          }
        }
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

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
        DamageCollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackEntities(_game)
          && _collisionSvc.TrySelectByEntitiesTag<SpikeTag, HeroTag>()
          && info.TargetCollider.Type == ColliderType.Body)
        {
          _message.CreateEntity()
            .Add((ref DamageMessage message) =>
            {
              message.Damage = _config.Damage;
              message.Master = info.PackedMaster;
              message.Target = info.PackedTarget;
            });
        }
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

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
        DamageCollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TrySelectByColliderTypes(ColliderType.Attack, ColliderType.Body)
          && _collisionSvc.TryUnpackEntities(_game)
          && !info.PackedMaster.EqualsTo(info.PackedTarget)
          && info.Master.Has<HeroTag>())
        {
          _message.CreateEntity()
            .Add((ref DamageMessage message) =>
            {
              message.Damage = _config.HitDamages[info.Master.Get<ComboAttackCounter>().Count];
              message.Master = info.PackedMaster;
              message.Target = info.PackedTarget;
            });
        }

        _collisionSvc.Reset();
      }
    }
  }
}
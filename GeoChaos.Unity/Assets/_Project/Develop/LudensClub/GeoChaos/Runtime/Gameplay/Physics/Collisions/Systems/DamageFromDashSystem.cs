using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions
{
  public class DamageFromDashSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly HeroConfig _config;
    private readonly EcsEntities _collisions;
    private readonly EcsWorld _game;

    public DamageFromDashSystem(MessageWorldWrapper messageWorldWrapper,
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
        if (_collisionSvc.TryUnpackEntities(_game)
          && _collisionSvc.TrySelectByColliderTypes(ColliderType.Dash, ColliderType.Body)
          && !info.PackedMaster.EqualsTo(info.PackedTarget)
          && info.Target.Has<Health>())
        {
          _message.CreateEntity()
            .Add((ref DamageMessage damage) =>
            {
              damage.Damage = _config.DashDamage;
              damage.Master = info.PackedMaster;
              damage.Target = info.PackedTarget;
            });
        }
        
        _collisionSvc.Reset();
      }
    }
  }
}
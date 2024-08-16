using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama.Attack
{
  public class DamageFromLamaBodySystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;
    private readonly LamaConfig _config;

    public DamageFromLamaBodySystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LamaConfig>();

      _collisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        _collisionSvc.AssignCollision(collision);
        CollisionInfo info = _collisionSvc.Info;
        if (_collisionSvc.TryUnpackBothEntities(_game)
          && _collisionSvc.TrySelectByEntitiesTag<LamaTag, HeroTag>()
          && info.MasterCollider.Type == ColliderType.Body
          && info.TargetCollider.Type == ColliderType.Body)
        {
          _message.CreateEntity()
            .Add((ref DamageMessage message) => message.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.DamageFromBody, info.MasterCollider.EntityPosition));
        }
      }
    }
  }
}
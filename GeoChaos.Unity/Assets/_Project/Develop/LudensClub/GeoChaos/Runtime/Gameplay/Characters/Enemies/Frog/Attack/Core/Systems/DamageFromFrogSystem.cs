using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Bite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Jump;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack
{
  public class DamageFromFrogSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;
    private readonly FrogConfig _config;

    public DamageFromFrogSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FrogConfig>();

      _collisions = _message
        .Filter<TwoSideCollision>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity col in _collisions)
      {
        ref TwoSideCollision collision = ref col.Get<TwoSideCollision>();
        CollisionInfo info = _collisionSvc.Info;
        _collisionSvc.AssignCollision(collision);
        if (_collisionSvc.TryUnpackBothEntities(_game)
          && _collisionSvc.TrySelectByEntitiesTag<FrogTag, HeroTag>()
          && info.TargetCollider.Type == ColliderType.Body)
        {
          if(TryGetDamage(info, out float damage))
          {
            _message.CreateEntity()
              .Add((ref DamageMessage message) => message.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
                damage, info.MasterCollider.EntityPosition));
          }
        }
      }
    }

    private bool TryGetDamage(CollisionInfo info, out float damage)
    {
      if (info.MasterCollider.Type == ColliderType.Body)
      {
        damage = info.Master.Has<JumpAttacking>() ? _config.DamageFromJump : _config.DamageFromBody;
        return true;
      }

      if (info.MasterCollider.Type == ColliderType.Attack && info.Master.Has<Biting>())
      {
        damage = _config.DamageFromBite;
        return true;
      }
      
      damage = 0;
      return false;
    }
  }
}
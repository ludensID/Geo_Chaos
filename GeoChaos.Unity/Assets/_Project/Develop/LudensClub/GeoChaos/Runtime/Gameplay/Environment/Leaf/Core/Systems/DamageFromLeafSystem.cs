﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Collisions;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class DamageFromLeafSystem : IEcsRunSystem
  {
    private readonly ICollisionService _collisionSvc;
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _collisions;
    private readonly LeafySpiritConfig _config;

    public DamageFromLeafSystem(MessageWorldWrapper messageWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      ICollisionService collisionSvc,
      IConfigProvider configProvider)
    {
      _collisionSvc = collisionSvc;
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

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
          && _collisionSvc.TrySelectByEntitiesTag<LeafTag, HeroTag>()
          && info.TargetCollider.Type == ColliderType.Body
          && info.Master.Has<Owner>()
          && info.Master.Get<Owner>().Entity.TryUnpackEntity(_game, out _))
        {
          _message.CreateEntity()
            .Add((ref DamageMessage message) => message.Info = new DamageInfo(info.PackedMaster, info.PackedTarget,
              _config.DamageFromLeaf, info.MasterCollider.EntityPosition));
        }
      }
    }
  }
}
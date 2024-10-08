﻿using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Shoot
{
  public class ShootSystem : IEcsRunSystem
  {
    private readonly IShardFactory _shardFactory;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly IShootService _shootSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly HeroConfig _config;
    private readonly EcsEntities _enemies;
    private readonly EcsEntity _createdShard;

    public ShootSystem(GameWorldWrapper gameWorldWrapper,
      IShardFactory shardFactory,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      ITimerFactory timers,
      IShootService shootSvc)
    {
      _shardFactory = shardFactory;
      _forceFactory = forceFactory;
      _timers = timers;
      _shootSvc = shootSvc;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<HeroTag>()
        .Inc<ShootCommand>()
        .Collect();

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<Damageable>()
        .Inc<Selected>()
        .Collect();

      _createdShard = new EcsEntity(_game);
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        Vector2 shootDirection = CalculateShootDirection(command);
        Vector3 position = command.Get<ViewRef>().View.transform.position;

        _createdShard.Entity = CreateShard(command.PackedEntity, _config.ShardLifeTime, position).Entity;
        
        SetVelocity(shootDirection, _createdShard.PackedEntity);

        AddCooldown(command);

        command.Del<ShootCommand>();
      }
    }

    private void AddCooldown(EcsEntity command)
    {
      command.Add((ref ShootCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.ShootCooldown));
    }

    private Vector2 CalculateShootDirection(EcsEntity command)
    {
      Vector2 shootDirection = _shootSvc.CalculateShootDirection(command.Get<ViewDirection>().Direction,
        command.Get<BodyDirection>().Direction);

      foreach (EcsEntity enemy in _enemies)
      {
        shootDirection = enemy.Get<ViewRef>().View.transform.position
          - command.Get<ViewRef>().View.transform.position;
      }

      if (command.Has<Aiming>())
        shootDirection = command.Get<ShootDirection>().Direction;

      shootDirection.Normalize();
      return shootDirection;
    }


    private EcsEntity CreateShard(EcsPackedEntity owner, float lifeTime, Vector3 position)
    {
      return _shardFactory.Create(position)
        .Add((ref Owner o) => o.Entity = owner)
        .Add((ref LifeTime lt) => lt.TimeLeft = _timers.Create(lifeTime));
    }

    private void SetVelocity(Vector2 shootDirection, EcsPackedEntity owner)
    {
      (Vector3 length, Vector3 direction) =
        MathUtils.DecomposeVector(shootDirection * _config.ShardVelocity);
      _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, owner, Vector2.one)
      {
        Speed = length,
        Direction = direction
      });
    }
  }
}
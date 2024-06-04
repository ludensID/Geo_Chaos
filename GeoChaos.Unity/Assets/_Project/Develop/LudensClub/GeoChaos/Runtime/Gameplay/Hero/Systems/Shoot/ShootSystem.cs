using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot.Aim;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Shard;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot
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
        .Filter<ShootCommand>()
        .Collect();

      _enemies = _game
        .Filter<Health>()
        .Inc<Selected>()
        .Exc<HeroTag>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        Vector2 shootDirection = CalculateShootDirection(command);
        Vector3 position = command.Get<ViewRef>().View.transform.position + (Vector3)shootDirection;

        EcsEntity shard = CreateShard(command.Pack(), _config.ShardLifeTime, position);

        SetVelocity(shootDirection, shard.Pack());

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
      return _shardFactory.Create()
        .Add((ref Owner o) => o.Entity = owner)
        .Add((ref LifeTime lt) => lt.TimeLeft = _timers.Create(lifeTime))
        .Replace((ref ViewRef viewRef) => viewRef.View.transform.position = position);
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
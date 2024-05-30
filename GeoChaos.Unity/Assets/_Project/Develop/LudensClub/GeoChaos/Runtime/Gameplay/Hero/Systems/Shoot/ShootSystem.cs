using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shot;
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
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;
    private readonly HeroConfig _config;
    private readonly EcsEntities _enemies;

    public ShootSystem(GameWorldWrapper gameWorldWrapper,
      IShardFactory shardFactory,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _shardFactory = shardFactory;
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _commands = _game
        .Filter<ShootCommand>()
        .Collect();

      _enemies = _game
        .Filter<EnemyTag>()
        .Inc<Selected>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands)
      {
        Vector2 shootDirection = CalculateShootDirection(command.Get<ViewDirection>().Direction,
          command.Get<BodyDirection>().Direction);
        foreach (EcsEntity enemy in _enemies)
        {
          shootDirection = enemy.Get<ViewRef>().View.transform.position
            - command.Get<ViewRef>().View.transform.position;
        }
        shootDirection.Normalize();
        
        Vector3 position = command.Get<ViewRef>().View.transform.position + (Vector3)shootDirection;

        EcsEntity shard = _shardFactory.Create()
          .Add((ref Owner owner) => owner.Entity = command.Pack())
          .Add((ref LifeTime lifeTime) => lifeTime.TimeLeft = _timers.Create(_config.ShardLifeTime))
          .Replace((ref ViewRef viewRef) => viewRef.View.transform.position = position);

        (Vector3 length, Vector3 direction) =
          MathUtils.DecomposeVector(shootDirection * _config.ShardVelocity);
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, shard.Pack(), Vector2.one)
        {
          Speed = length,
          Direction = direction
        });

        command.Del<ShootCommand>();
      }
    }

    private Vector2 CalculateShootDirection(Vector2 viewDirection, float bodyDirection)
    {
      Vector2 shootDirection = viewDirection;
      shootDirection.y = MathUtils.Clamp(shootDirection.y, 0);

      if (shootDirection == Vector2.zero)
        shootDirection = Vector2.right * bodyDirection;

      return shootDirection;
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public DashHeroSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _config = configProvider.Get<HeroConfig>();
      _game = gameWorldWrapper.World;

      _heroes = _game.Filter<HeroTag>()
        .Inc<DashCommand>()
        .Inc<MovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Dash, hero.Pack(), Vector2.one)
        {
          Speed = new Vector2(_config.DashVelocity, 0),
          Direction = hero.Get<MovementVector>().Direction,
          Unique = true,
          Immutable = true
        });

        hero
          .Add((ref Dashing dashing) => dashing.TimeLeft = _timers.Create(_config.DashTime))
          .Add<LockMovementCommand>()
          .Replace((ref GravityScale gravity) => gravity.Enabled = false);
      }
    }
  }
}
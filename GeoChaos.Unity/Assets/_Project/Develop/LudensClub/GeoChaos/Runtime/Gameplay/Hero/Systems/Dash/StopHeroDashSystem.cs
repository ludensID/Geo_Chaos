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
  public class StopHeroDashSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly HeroConfig _config;
    private readonly EcsEntities _heroes;

    public StopHeroDashSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory, ITimerFactory timers, IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game.Filter<HeroTag>()
        .Inc<StopDashCommand>()
        .Inc<IsDashing>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Dash, hero.Pack(), true)
        {
          Speed = Vector2.zero,
          Instant = true
        });
        
        hero
          .Del<IsDashing>()
          .Add<UnlockMovementCommand>()
          .Add((ref DashCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.DashCooldown));
      }
    }
  }
}
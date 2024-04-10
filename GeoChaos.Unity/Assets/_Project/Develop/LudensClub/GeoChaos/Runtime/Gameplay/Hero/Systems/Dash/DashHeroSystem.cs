using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public DashHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _config = configProvider.Get<HeroConfig>();
      _game = gameWorldWrapper.World;

      _heroes = _game.Filter<Hero>()
        .Inc<DashCommand>()
        .Inc<MovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var vector = ref _game.Get<MovementVector>(hero);
        vector.Speed.x = _config.DashVelocity;
        vector.Speed.y = 0;

        ref var isDashing = ref _game.Add<IsDashing>(hero);
        isDashing.TimeLeft = _timers.Create(_config.DashTime);

        _game.Add<LockMovementCommand>(hero);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public DashHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _config = configProvider.Get<HeroConfig>();
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<Hero>()
        .Inc<DashCommand>()
        .Inc<HeroMovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var vector = ref _world.Get<HeroMovementVector>(hero);
        vector.Speed.x = _config.DashVelocity;
        vector.Speed.y = 0;

        ref var isDashing = ref _world.Add<IsDashing>(hero);
        isDashing.TimeLeft = _timers.Create(_config.DashTime);

        // stop jumping
        if (_world.Has<IsJumping>(hero))
          _world.Del<IsJumping>(hero);

        if (_world.Has<Movable>(hero))
          _world.Del<Movable>(hero);

        if (_world.Has<JumpAvailable>(hero))
          _world.Del<JumpAvailable>(hero);
      }
    }
  }
}
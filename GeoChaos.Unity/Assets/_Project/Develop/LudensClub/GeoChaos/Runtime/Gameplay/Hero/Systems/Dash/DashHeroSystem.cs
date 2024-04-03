using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class DashHeroSystem : IEcsRunSystem
  {
    private readonly ITimerService _timerSvc;
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public DashHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerService timerSvc)
    {
      _timerSvc = timerSvc;
      _config = configProvider.Get<HeroConfig>();
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<Hero>()
        .Inc<DashCommand>()
        .Inc<HeroMovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        vector.Speed.x = _config.DashVelocity;
        vector.Speed.y = 0;
        
        ref IsDashing isDashing = ref _world.Add<IsDashing>(hero);
        isDashing.TimeLeft = _config.DashTime;
        _timerSvc.AddTimer(isDashing.TimeLeft);
        
        // stop jumping
        if(_world.Has<IsJumping>(hero))
          _world.Del<IsJumping>(hero);

        if (_world.Has<WaitToStopJump>(hero))
          _world.Del<WaitToStopJump>(hero);

        if (_world.Has<Movable>(hero))
          _world.Del<Movable>(hero);
        
        if(_world.Has<JumpAvailable>(hero))
          _world.Del<JumpAvailable>(hero);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class StopHeroDashSystem : IEcsRunSystem
  {
    private readonly ITimerService _timerSvc;
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public StopHeroDashSystem(GameWorldWrapper gameWorldWrapper, ITimerService timerSvc, IConfigProvider configProvider)
    {
      _timerSvc = timerSvc;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _game.Filter<Hero>()
        .Inc<StopDashCommand>()
        .Inc<IsDashing>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        _game.Add<Movable>(hero);
        _game.Add<JumpAvailable>(hero);

        _game.Del<IsDashing>(hero);

        ref DashCooldown cooldown = ref _game.Add<DashCooldown>(hero);
        cooldown.Timer = _config.DashCooldown;
        _timerSvc.AddTimer(cooldown.Timer);
      }
    }
  }
}
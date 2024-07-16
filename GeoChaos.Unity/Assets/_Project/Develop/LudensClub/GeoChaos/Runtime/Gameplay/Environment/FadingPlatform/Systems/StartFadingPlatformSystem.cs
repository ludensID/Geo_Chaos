using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class StartFadingPlatformSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _fadeCommands;
    private readonly FadingPlatformConfig _config;

    public StartFadingPlatformSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FadingPlatformConfig>();

      _fadeCommands = _game
        .Filter<StartFadeCommand>()
        .Inc<FadingPlatformTag>()
        .Exc<FadeTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _fadeCommands)
      {
        command.Add((ref FadeTimer timer) => timer.TimeLeft = _timers.Create(_config.FadeTime));
      }
    }
  }
}
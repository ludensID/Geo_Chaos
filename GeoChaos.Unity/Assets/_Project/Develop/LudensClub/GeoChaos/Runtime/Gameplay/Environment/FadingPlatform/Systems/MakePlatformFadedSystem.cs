using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.FadingPlatform
{
  public class MakePlatformFadedSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly FadingPlatformConfig _config;
    private readonly EcsEntities _fadeTimers;

    public MakePlatformFadedSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<FadingPlatformConfig>();

      _fadeTimers = _game
        .Filter<FadingPlatformTag>()
        .Inc<FadeTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity fade in _fadeTimers
        .Check<FadeTimer>(x => x.TimeLeft <= 0))
      {
        fade
          .Del<FadeTimer>()
          .Add((ref AppearCooldown cooldown) => cooldown.TimeLeft = _timers.Create(_config.AppearCooldown));
      }
    }
  }
}
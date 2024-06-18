using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  public class ReleaseRingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _hookedRings;
    private readonly HeroConfig _config;
    private readonly EcsWorld _message;
    private readonly EcsEntities _messages;

    public ReleaseRingSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper,
      ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _messages = _message
        .Filter<ReleaseRingMessage>()
        .Collect();

      _hookedRings = _game
        .Filter<RingTag>()
        .Inc<Hooked>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity message in _messages)
      {
        foreach (EcsEntity ring in _hookedRings)
        {
          ring
            .Del<Hooked>()
            .Add((ref Releasing releasing) => releasing.TimeLeft = _timers.Create(_config.RingReleasingTime));
        }

        message.Dispose();
      }
    }
  }
}
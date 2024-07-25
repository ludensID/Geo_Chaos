using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Leap
{
  public class PrecastLeafySpiritLeapSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirit;
    private readonly LeafySpiritConfig _config;

    public PrecastLeafySpiritLeapSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _leapingSpirit = _game
        .Filter<LeafySpiritTag>()
        .Inc<LeapCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirit)
      {
        spirit
          .Del<LeapCommand>()
          .Add<Leaping>()
          .Add((ref PrecastLeapTimer timer) => timer.TimeLeft = _timers.Create(_config.PrecastLeapTime));
      }
    }
  }
}
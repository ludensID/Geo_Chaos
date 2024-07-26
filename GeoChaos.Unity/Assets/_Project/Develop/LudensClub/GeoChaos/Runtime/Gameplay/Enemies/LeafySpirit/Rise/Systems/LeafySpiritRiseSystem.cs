using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Rise
{
  public class LeafySpiritRiseSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _risingSpirits;
    private readonly LeafySpiritConfig _config;

    public LeafySpiritRiseSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _risingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<RiseCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _risingSpirits)
      {
        spirit
          .Del<RiseCommand>()
          .Add<OnRiseStarted>()
          .Add<Rising>()
          .Add((ref RiseTimer timer) => timer.TimeLeft = _timers.Create(_config.RisingTime));
      }
    }
  }
}
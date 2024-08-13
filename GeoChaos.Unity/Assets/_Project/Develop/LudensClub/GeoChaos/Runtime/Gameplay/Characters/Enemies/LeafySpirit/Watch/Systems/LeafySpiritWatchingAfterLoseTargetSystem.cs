using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Watch
{
  public class LeafySpiritWatchingAfterLoseTargetSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _aimedSpirits;
    private ITimerFactory _timers;
    private readonly LeafySpiritConfig _config;

    public LeafySpiritWatchingAfterLoseTargetSystem(GameWorldWrapper gameWorldWrapper, ITimerFactory timers, IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _aimedSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<WasAimed>()
        .Exc<Aimed>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _aimedSpirits)
      {
        spirit.Add((ref WatchingTimer timer) => timer.TimeLeft = _timers.Create(_config.WatchingTime));
      }
    }
  }
}
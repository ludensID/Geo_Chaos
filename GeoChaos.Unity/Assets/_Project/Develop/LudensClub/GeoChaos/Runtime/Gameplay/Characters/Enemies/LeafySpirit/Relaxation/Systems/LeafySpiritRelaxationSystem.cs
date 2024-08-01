using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Relaxation
{
  public class LeafySpiritRelaxationSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _relaxingSpirits;
    private readonly LeafySpiritConfig _config;

    public LeafySpiritRelaxationSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider, ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();
      
      _relaxingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<RelaxCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _relaxingSpirits)
      {
        spirit
          .Del<RelaxCommand>()
          .Add<Relaxing>()
          .Add((ref RelaxationTimer timer) => timer.TimeLeft = _timers.Create(_config.RelaxationTime));
      }
    }
  }
}
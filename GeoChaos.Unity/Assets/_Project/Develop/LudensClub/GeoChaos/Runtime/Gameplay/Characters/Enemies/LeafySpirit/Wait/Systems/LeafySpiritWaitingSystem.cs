using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Wait
{
  public class LeafySpiritWaitingSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _waitingSpirits;
    private readonly LeafySpiritConfig _config;

    public LeafySpiritWaitingSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();
      
      _waitingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<WaitCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _waitingSpirits)
      {
        spirit
          .Del<WaitCommand>()
          .Add((ref WaitingTimer timer) => timer.TimeLeft = _timers.Create(_config.WaitingTime));
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide
{
  public class LeafySpiritBidingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bidingSpirits;
    private ITimerFactory _timers;
    private LeafySpiritConfig _config;

    public LeafySpiritBidingSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _bidingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<BideCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _bidingSpirits)
      {
        spirit
          .Del<BideCommand>()
          .Add<Biding>()
          .Add((ref BidingTimer timer) => timer.TimeLeft = _timers.Create(_config.WaitAfterAttackTime));
      }
    }
  }
}
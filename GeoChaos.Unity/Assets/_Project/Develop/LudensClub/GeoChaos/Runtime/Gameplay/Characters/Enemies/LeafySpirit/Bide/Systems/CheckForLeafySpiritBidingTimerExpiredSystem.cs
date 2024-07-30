using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide
{
  public class CheckForLeafySpiritBidingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bidingSpirits;

    public CheckForLeafySpiritBidingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bidingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<BidingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _bidingSpirits
        .Check<BidingTimer>(x => x.TimeLeft <= 0))
      {
        spirit
          .Del<BidingTimer>()
          .Del<Biding>()
          .Add<OnBidingFinished>();
      }
    }
  }
}
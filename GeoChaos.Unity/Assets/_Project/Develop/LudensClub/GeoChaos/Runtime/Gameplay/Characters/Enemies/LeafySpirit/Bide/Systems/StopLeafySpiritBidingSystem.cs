using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Bide
{
  public class StopLeafySpiritBidingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _bidingSpirits;

    public StopLeafySpiritBidingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _bidingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopBideCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _bidingSpirits)
      {
        spirit
          .Del<StopBideCommand>()
          .Has<Biding>(false)
          .Has<BidingTimer>(false);
      }
    }
  }
}
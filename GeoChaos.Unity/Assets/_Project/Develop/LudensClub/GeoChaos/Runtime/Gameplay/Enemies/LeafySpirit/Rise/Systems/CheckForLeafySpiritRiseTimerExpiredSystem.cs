using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Rise
{
  public class CheckForLeafySpiritRiseTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _risingSpirits;

    public CheckForLeafySpiritRiseTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _risingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<RiseTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _risingSpirits
        .Where<RiseTimer>(x => x.TimeLeft <= 0))
      {
        spirit
          .Del<RiseTimer>()
          .Del<Rising>()
          .Add<Risen>();
      }
    }
  }
}
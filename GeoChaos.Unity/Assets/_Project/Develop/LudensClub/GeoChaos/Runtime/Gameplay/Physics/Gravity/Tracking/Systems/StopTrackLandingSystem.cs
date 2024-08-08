using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking
{
  public class StopTrackLandingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _trackingEntities;

    public StopTrackLandingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _trackingEntities = _game
        .Filter<StopTrackLandingCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity track in _trackingEntities)
      {
        track
          .Del<StopTrackLandingCommand>()
          .Has<TrackingLifting>(false)
          .Has<TrackingLanding>(false);
      }
    }
  }
}
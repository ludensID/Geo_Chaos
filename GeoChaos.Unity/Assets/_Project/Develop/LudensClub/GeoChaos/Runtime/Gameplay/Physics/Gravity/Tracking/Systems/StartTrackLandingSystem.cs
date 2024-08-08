using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity.Tracking
{
  public class StartTrackLandingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _trackingEntities;

    public StartTrackLandingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _trackingEntities = _game 
        .Filter<TrackLandingCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity track in _trackingEntities)
      {
        track.Del<TrackLandingCommand>();
        
        bool onGround = track.Has<OnGround>();
        track
          .Has<TrackingLifting>(onGround)
          .Has<TrackingLanding>(!onGround);
      }
    }
  }
}
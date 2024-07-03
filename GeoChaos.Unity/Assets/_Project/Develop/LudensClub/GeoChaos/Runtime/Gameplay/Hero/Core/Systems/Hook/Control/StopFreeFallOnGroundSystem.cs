using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopFreeFallOnGroundSystem : IEcsRunSystem
  {
    private readonly IFreeFallService _freeFallSvc;
    private readonly EcsWorld _game;
    private readonly EcsEntities _onGrounds;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _freeFalls;

    public StopFreeFallOnGroundSystem(GameWorldWrapper gameWorldWrapper,
      PhysicsWorldWrapper physicsWorldWrapper,
      IFreeFallService freeFallSvc)
    {
      _freeFallSvc = freeFallSvc;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _onGrounds = _game
        .Filter<FreeFalling>()
        .Inc<OnGround>()
        .Collect();
      
      _freeFalls = _physics
        .Filter<FreeFall>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _onGrounds)
      {
        _freeFallSvc.StopFreeFall(ground, _freeFalls);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopFreeFallOnGroundSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _onGrounds;

    public StopFreeFallOnGroundSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _onGrounds = _game
        .Filter<FreeFalling>()
        .Inc<OnGround>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _onGrounds)
      {
        ground.Add<StopFallFreeCommand>();
      }
    }
  }
}
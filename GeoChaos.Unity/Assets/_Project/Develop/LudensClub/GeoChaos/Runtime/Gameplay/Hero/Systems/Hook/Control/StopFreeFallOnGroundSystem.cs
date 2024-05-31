using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class StopFreeFallOnGroundSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _onGrounds;

    public StopFreeFallOnGroundSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _onGrounds = _game
        .Filter<HookFalling>()
        .Inc<OnGround>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity ground in _onGrounds)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, ground.Pack()));
        ground.Add<StopFallFreeCommand>();
      }
    }
  }
}
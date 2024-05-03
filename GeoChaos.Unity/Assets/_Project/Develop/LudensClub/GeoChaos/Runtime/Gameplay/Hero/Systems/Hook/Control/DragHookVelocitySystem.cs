using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DragHookVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _draggable;
    private readonly SpeedForceLoop _forceLoop;
    private readonly HeroConfig _config;

    public DragHookVelocitySystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forceLoop = forceLoopSvc.CreateLoop();

      _draggable = _game
        .Filter<DragForceAvailable>()
        .Inc<DragForceFactor>()
        .Inc<DragForcing>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity draggable in _draggable)
      {
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Hook, draggable.Pack()))
        {
          float dragForce = -draggable.Get<DragForceFactor>().Factor * _config.DragForceMultiplier;
          if (_config.IsRelativeHookSpeed)
            dragForce *= draggable.Get<DragForcing>().SpeedX;
          force.Replace((ref Acceleration acceleration) =>
            acceleration.Value = Vector2.one * dragForce);
        }
      }
    }
  }
}
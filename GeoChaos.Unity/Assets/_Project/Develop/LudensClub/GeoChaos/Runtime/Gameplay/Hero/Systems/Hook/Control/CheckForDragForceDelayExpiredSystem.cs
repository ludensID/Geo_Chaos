using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  [Obsolete]
  public class CheckForDragForceDelayExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _delays;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForDragForceDelayExpiredSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceLoopService forceLoopSvc,
      IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forceLoop = forceLoopSvc.CreateLoop();

      _delays = _game
        .Filter<Hooking>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delay in _delays)
      {

        // if (delay.Is<Controllable>())
        // {
        //   float maxSpeed = 0;
        //   float acceleration = 0;
        //   foreach(EcsEntity force in _forceLoop.GetLoop(SpeedForceType.Move, delay.Pack()))
        //   {
        //     force.Add<Added>();
        //     maxSpeed = force.Get<MaxSpeed>().Speed;
        //     acceleration = force.Get<Acceleration>().Value.x;
        //   }
        //   
        //   
        //   delay
        //     .Add((ref Controlling controlling) =>
        //     {
        //       controlling.Rate = 1 / controlTime;
        //       controlling.MaxSpeed = maxSpeed;
        //       controlling.Acceleration = acceleration;
        //     })
        //     .Replace((ref ControlFactor factor) => factor.Factor = 0);
        // }
      }
    }
  }
}
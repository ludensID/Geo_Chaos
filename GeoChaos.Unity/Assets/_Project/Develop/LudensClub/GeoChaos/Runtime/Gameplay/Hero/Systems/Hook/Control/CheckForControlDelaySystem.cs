using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class CheckForControlDelaySystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _delays;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForControlDelaySystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceLoopService forceLoopSvc,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forceLoop = forceLoopSvc.CreateLoop();

      _delays = _game
        .Filter<ControlDelay>()
        .Inc<HookPulling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delay in _delays
        .Where<ControlDelay>(x => x.TimeLeft <= 0))
      {
        delay.Del<ControlDelay>();
        EcsEntity hookForce = _forceLoop.GetForce(SpeedForceType.Hook, delay.Pack());
        hookForce
          .Del<Unique>()
          .Del<Immutable>();

        float controlTime = delay.Get<HookPulling>().Time * (1 - _config.StartControlCoefficient) * 2;


        if (delay.Is<DragForceAvailable>())
        {
          delay
            .Add((ref DragForcing forcing) =>
            {
              forcing.Rate = 1 / controlTime;
              forcing.SpeedX = Mathf.Abs(delay.Get<HookPulling>().Velocity.x);
            })
            .Replace((ref DragForceFactor factor) => factor.Factor = 0);

          hookForce
            .Add<Acceleration>()
            .Add((ref MaxSpeed speed) => speed.Speed = hookForce.Get<MovementVector>().Speed.magnitude);
        }

        if (delay.Is<Controllable>())
        {
          float maxSpeed = 0;
          float acceleration = 0;
          foreach(EcsEntity force in _forceLoop.GetLoop(SpeedForceType.Move, delay.Pack()))
          {
            force.Add<Added>();
            maxSpeed = force.Get<MaxSpeed>().Speed;
            acceleration = force.Get<Acceleration>().Value.x;
          }
          
          
          delay
            .Add((ref Controlling controlling) =>
            {
              controlling.Rate = 1 / controlTime;
              controlling.MaxSpeed = maxSpeed;
              controlling.Acceleration = acceleration;
            })
            .Replace((ref ControlFactor factor) => factor.Factor = 0);
        }
      }
    }
  }
}
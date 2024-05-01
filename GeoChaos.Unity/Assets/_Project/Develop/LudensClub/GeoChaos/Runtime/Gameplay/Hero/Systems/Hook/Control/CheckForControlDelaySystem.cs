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
    private readonly EcsWorld _game;
    private readonly EcsEntities _delays;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForControlDelaySystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc,
      IConfigProvider configProvider)
    {
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
        float controlTime = delay.Get<HookPulling>().Time * (1 - _config.StartControlCoefficient) * 2;
        delay
          .Del<ControlDelay>()
          .Replace((ref ControlFactor factor) => factor.Factor = 0);

        if (delay.Is<DragForceAvailable>())
        {
          delay
            .Add((ref DragForcing forcing) =>
            {
              forcing.Rate = 1 / controlTime;
              forcing.SpeedX = Mathf.Abs(delay.Get<HookPulling>().Velocity.x);
            })
            .Replace((ref DragForceFactor factor) => factor.Factor = 0);
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Hook, delay.Pack()))
          {
            force
              .Add<Acceleration>()
              .Add((ref MaxSpeed speed) => speed.Speed = force.Get<MovementVector>().Speed.magnitude);
          }
        }
      }
    }
  }
}
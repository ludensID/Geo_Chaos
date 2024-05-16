using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CalculateControlSpeedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _controls;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forces;

    public CalculateControlSpeedSystem(PhysicsWorldWrapper physicsWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _forces = forceLoopSvc.CreateLoop();

      _controls = _physics
        .Filter<ADControl>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity control in _controls)
      {
        if (control.Has<Enabled>() || control.Has<Prepared>())
        {
          float direction = 0;
          foreach (var move in _forces
            .GetLoop(SpeedForceType.Move, control.Get<Owner>().Entity))
          {
            move.Has<Ignored>(true);
            direction = move.Get<MovementVector>().Direction.x;
          }

          float accelerate = control.Get<Gradient>().Value * _config.ADControlSpeed * direction;
          control.Replace((ref AccelerationSpeed acceleration) => acceleration.Acceleration = accelerate);
          ref ControlSpeed speed = ref control.Get<ControlSpeed>();
          if (direction == 0)
          {
            speed.Speed = 0;
          }
          else
          {
              speed.Speed += accelerate * Time.fixedDeltaTime;
              speed.Speed = MathUtils.Clamp(speed.Speed, -Mathf.Abs(accelerate), Mathf.Abs(accelerate));
          }
        }
        else
        {
          foreach (var move in _forces
            .GetLoop(SpeedForceType.Move, control.Get<Owner>().Entity))
          {
            move.Has<Ignored>(false);
          }
        }
      }
    }
  }
}
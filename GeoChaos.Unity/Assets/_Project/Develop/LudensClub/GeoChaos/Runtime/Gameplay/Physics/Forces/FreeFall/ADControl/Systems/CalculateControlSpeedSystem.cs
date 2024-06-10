using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CalculateControlSpeedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _controls;
    private readonly HeroConfig _config;
    private readonly SpeedForceLoop _forces;
    private readonly EcsWorld _game;

    public CalculateControlSpeedSystem(PhysicsWorldWrapper physicsWorldWrapper,
      GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _physics = physicsWorldWrapper.World;
      _game = gameWorldWrapper.World;
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
        bool needCount = control.Has<Enabled>() || control.Has<Prepared>();
        bool hasMove = false;

        foreach (EcsEntity move in _forces
          .GetLoop(SpeedForceType.Move, control.Get<Owner>().Entity))
        {
          move.Has<Ignored>(needCount);
          hasMove = true;
        }

        float direction = 0;
        if (hasMove && control.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity owner))
          direction = owner.Get<MoveDirection>().Direction.x;

        if (needCount)
        {
          float acceleration = control.Get<Gradient>().Value * _config.ADControlSpeed * direction;
          control.Replace(
            (ref ControlSpeed speed) => speed.Speed = CalculateSpeed(speed.Speed, acceleration, direction));
        }
      }
    }

    private float CalculateSpeed(float speed, float acceleration, float direction)
    {
      float abs = Mathf.Abs(acceleration);
      return direction != 0 ? Mathf.Clamp(speed + acceleration * Time.fixedDeltaTime, -abs, abs) : 0;
    }
  }
}
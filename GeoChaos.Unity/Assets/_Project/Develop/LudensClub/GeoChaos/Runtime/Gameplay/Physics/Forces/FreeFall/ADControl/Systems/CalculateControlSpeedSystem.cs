using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Move;
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
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity control in _controls)
      {
        EcsPackedEntity packedOwner = control.Get<Owner>().Entity;
        bool hasMove = _forces.GetLoop(SpeedForceType.Move, packedOwner).Any();

        float direction = 0;
        if (hasMove && packedOwner.TryUnpackEntity(_game, out EcsEntity owner))
          direction = owner.Get<MoveDirection>().Direction.x;

        float acceleration = control.Get<Gradient>().Value * _config.ADControlSpeed * direction;
        control.Replace(
          (ref ControlSpeed speed) => speed.Speed = CalculateSpeed(speed.Speed, acceleration, direction));
      }
    }

    private float CalculateSpeed(float speed, float acceleration, float direction)
    {
      float abs = Mathf.Abs(acceleration);
      return direction != 0 ? Mathf.Clamp(speed + acceleration * Time.fixedDeltaTime, -abs, abs) : 0;
    }
  }
}
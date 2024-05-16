using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class IncreaseDragForceGradientSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities drags;
    private readonly HeroConfig _config;

    public IncreaseDragForceGradientSystem(PhysicsWorldWrapper physicsWorldWrapper, IConfigProvider configProvider)
    {
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      drags = _physics
        .Filter<DragForce>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity drag in drags)
      {
        Vector2 multiplier = drag.Get<Gradient>().Value * _config.DragForceMultiplier;
        Vector2 forceValue = _config.IsRelativeSpeed
          ? multiplier * drag.Get<RelativeSpeed>().Speed
          : multiplier;
        drag.Replace((ref DragForce force) => force.Force = forceValue);
      }
    }
  }
}
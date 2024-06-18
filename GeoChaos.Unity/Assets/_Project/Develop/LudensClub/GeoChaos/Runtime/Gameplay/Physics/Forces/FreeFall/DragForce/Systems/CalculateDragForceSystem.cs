using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CalculateDragForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _drags;
    private readonly HeroConfig _config;

    public CalculateDragForceSystem(PhysicsWorldWrapper physicsWorldWrapper, IConfigProvider configProvider)
    {
      _physics = physicsWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _drags = _physics
        .Filter<DragForce>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity drag in _drags)
      {
        drag.Replace((ref DragForce force) => force.Force = GetForce(drag));
      }
    }

    private Vector2 GetForce(EcsEntity drag)
    {
      Vector2 forceVector = drag.Get<Gradient>().Value * _config.DragForceMultiplier;
      if (_config.IsRelativeSpeed)
        forceVector *= drag.Get<RelativeSpeed>().Speed;
      return forceVector;
    }
  }
}
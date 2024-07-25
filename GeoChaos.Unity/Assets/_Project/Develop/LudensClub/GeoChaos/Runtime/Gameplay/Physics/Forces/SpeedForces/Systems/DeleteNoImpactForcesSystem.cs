using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DeleteNoImpactForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _noImpactForces;

    public DeleteNoImpactForcesSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _noImpactForces = _physics
        .Filter<Impact>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity force in _noImpactForces
        .Check<Impact>(x => x.Vector == Vector2.zero))
      {
        force.Dispose();
      }
    }
  }
}
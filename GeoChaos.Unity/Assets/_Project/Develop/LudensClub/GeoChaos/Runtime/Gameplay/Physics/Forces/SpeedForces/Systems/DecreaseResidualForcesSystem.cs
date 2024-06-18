using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class DecreaseResidualForcesSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _vectors;
    private readonly EcsEntities _forces;

    public DecreaseResidualForcesSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _vectors = _game
        .Filter<MovementVector>()
        .Inc<LastMovementVector>()
        .Collect();

      _forces = _physics
        .Filter<SpeedForce>()
        .Inc<Residual>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        ref MovementVector movementVector = ref vector.Get<MovementVector>();
        ref LastMovementVector lastMovementVector = ref vector.Get<LastMovementVector>();
        Vector2 delta = MathUtils.DecomposeVector(lastMovementVector.Direction * lastMovementVector.Speed
          - movementVector.Direction * movementVector.Speed).length;
        foreach (EcsEntity force in _forces
          .Where<Owner>(x => x.Entity.EqualsTo(vector.Pack())))
        {
          ref Impact impact = ref force.Get<Impact>();
          ref MovementVector forceVector = ref force.Get<MovementVector>();
          for (int i = 0; i < 2; i++)
          {
            if (impact.Vector[i] != 0)
            {
              float temp = delta[i];
              delta[i] = MathUtils.DecreaseToZero(delta[i], forceVector.Speed[i]);
              forceVector.Speed[i] = MathUtils.DecreaseToZero(forceVector.Speed[i], temp);
            }
          }
        }
      }
    }
  }
}
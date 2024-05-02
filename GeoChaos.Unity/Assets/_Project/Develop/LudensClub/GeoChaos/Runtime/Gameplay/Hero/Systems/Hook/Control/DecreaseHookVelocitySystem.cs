using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DecreaseHookVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _physics;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _dragging;
    private readonly EcsEntities _addedForces;

    public DecreaseHookVelocitySystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceLoopService forceLoopSvc,
      PhysicsWorldWrapper physicsWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;
      
      _forceLoop = forceLoopSvc.CreateLoop();

      _dragging = _game
        .Filter<DragForcing>()
        .Collect();

      _addedForces = _physics
        .Filter<SpeedForce>()
        .Inc<Added>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity dragging in _dragging)
      {
        foreach (EcsEntity hook in _forceLoop
          .GetLoop(SpeedForceType.Hook, dragging.Pack()))
        {
          ref MovementVector hookVector = ref hook.Get<MovementVector>();
          foreach (EcsEntity added in _addedForces
            .Where<Owner>(x => x.Entity.EqualsTo(dragging.Pack())))
          {
            ref MovementVector addedVector = ref added.Get<MovementVector>();
            if (hookVector.Direction.x != addedVector.Direction.x)
              hookVector.Speed.x -= addedVector.Speed.x * Time.fixedDeltaTime;
          }
        }
      }
    }
  }
}
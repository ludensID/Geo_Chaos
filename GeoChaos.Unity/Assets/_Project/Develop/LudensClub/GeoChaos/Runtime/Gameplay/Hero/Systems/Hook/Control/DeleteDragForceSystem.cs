using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DeleteDragForceSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _draggables;
    private readonly EcsWorld _physics;
    private readonly EcsEntities _dragForces;

    public DeleteDragForceSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _draggables = _game
        .Filter<DragForceAvailable>()
        .Collect();

      _dragForces = _physics
        .Filter<DragForce>()
        .Inc<Enabled>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity draggable in _draggables)
      {
        ref MovementVector entityVector = ref draggable.Get<MovementVector>();
        bool fullControl = draggable.Is<OnGround>();

        if (!draggable.Is<HookPulling>())
        {
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Hook, draggable.Pack()))
          {
            ref MovementVector forceVector = ref force.Get<MovementVector>();
            if ((!force.Is<Instant>() && forceVector.Speed.x <= 0) || fullControl)
            {
              force
                .Replace((ref MovementVector vector) => vector.Speed.x = 0)
                .Add<Instant>();
            }
          }
        }

        if (fullControl)
        {
          foreach (EcsEntity drag in _dragForces
            .Where<Owner>(x => x.Entity.EqualsTo(draggable.Pack())))
          {
            drag.Is<DragForceDelay>(false)
              .Is<Enabled>(false);
          }
        }

        // if (draggable.Is<Controlling>())
        // {
        //   bool fullControl = draggable.Is<OnGround>();
        //   foreach (EcsEntity force in _forceLoop
        //     .GetLoop(SpeedForceType.Move, draggable.Pack()))
        //   {
        //     ref MovementVector forceVector = ref force.Get<MovementVector>();
        //     if (((draggable.Is<DragForceAvailable>() ||
        //         draggable.Get<ControlFactor>().Factor >= 1) && !draggable.Is<DragForcing>()) ||
        //       fullControl)
        //     {
        //       fullControl = true;
        //       force.Del<Added>();
        //     }
        //   }
        //
        //   if (fullControl)
        //     draggable.Del<Controlling>();
        // }
      }
    }
  }
}
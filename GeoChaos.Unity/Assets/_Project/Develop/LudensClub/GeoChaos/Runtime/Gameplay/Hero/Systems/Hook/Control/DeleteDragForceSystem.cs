using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DeleteDragForceSystem : IEcsRunSystem
  {
    private readonly IDragForceService _dragForceSvc;
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _draggables;
    private readonly EcsWorld _physics;

    public DeleteDragForceSystem(GameWorldWrapper gameWorldWrapper, PhysicsWorldWrapper physicsWorldWrapper,
      ISpeedForceLoopService forceLoopSvc, IDragForceService dragForceSvc, ISpeedForceFactory forceFactory)
    {
      _dragForceSvc = dragForceSvc;
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _physics = physicsWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _draggables = _game
        .Filter<DragForceAvailable>()
        .Inc<HookFalling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity draggable in _draggables)
      {
        bool fullControl = draggable.Has<OnGround>();

        if (fullControl)
        {
          // _forceFactory.Create(new SpeedForceData(SpeedForceType.Hook, draggable.Pack(), Vector2.right));
          _dragForceSvc.GetDragForce(draggable.Pack())
            .Has<Enabled>(false);
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
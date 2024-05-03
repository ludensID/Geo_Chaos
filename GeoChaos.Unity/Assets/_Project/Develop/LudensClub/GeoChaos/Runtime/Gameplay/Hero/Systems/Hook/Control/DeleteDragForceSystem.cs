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
    private readonly EcsEntities _vectors;

    public DeleteDragForceSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _vectors = _game
        .Filter<MovementVector>()
        .Exc<HookPulling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        ref MovementVector entityVector = ref vector.Get<MovementVector>();
        float entityVelocityX = entityVector.Speed.x * entityVector.Direction.x;
        if (vector.Is<DragForcing>())
        {
          bool fullControl = vector.Is<OnGround>();
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Hook, vector.Pack()))
          {
            ref MovementVector forceVector = ref force.Get<MovementVector>();
            if ((!force.Is<Instant>() && forceVector.Speed.x <= 0) || fullControl)
            {
              force
                .Replace((ref MovementVector vector) => vector.Speed.x = 0)
                .Add<Instant>();
            }
          }

          if (fullControl)
            vector.Del<DragForcing>();
        }

        if (vector.Is<Controlling>())
        {
          bool fullControl = vector.Is<OnGround>();
          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Move, vector.Pack()))
          {
            ref MovementVector forceVector = ref force.Get<MovementVector>();
            if (((vector.Is<DragForceAvailable>() ||
              vector.Get<ControlFactor>().Factor >= 1) && !vector.Is<DragForcing>()) ||
              fullControl)
            {
              fullControl = true;
              force.Del<Added>();
            }
          }

          if (fullControl)
            vector.Del<Controlling>();
        }
      }
    }
  }
}
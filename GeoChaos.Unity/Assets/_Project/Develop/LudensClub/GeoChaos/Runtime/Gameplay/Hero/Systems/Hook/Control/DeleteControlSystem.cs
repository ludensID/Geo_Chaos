using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Hook
{
  public class DeleteControlSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _controllings;

    public DeleteControlSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;

      _forceLoop = forceLoopSvc.CreateLoop();

      _controllings = _game
        .Filter<DragForcing>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity controlling in _controllings)
      {
        var isControl = false;
        ref MovementVector entityVector = ref controlling.Get<MovementVector>();
        float entityVelocityX = entityVector.Speed.x * entityVector.Direction.x;
        foreach (var force in _forceLoop
          .GetLoop(SpeedForceType.Hook, controlling.Pack()))
        {
          ref MovementVector forceVector = ref force.Get<MovementVector>();
          if (forceVector.Speed.x <= 0 || entityVelocityX == 0)
          {
            force.Replace((ref Impact impact) => impact.X = false);
            isControl = true;
          }
        }

        if (isControl)
          controlling.Del<DragForcing>();
      }
    }
  }
}
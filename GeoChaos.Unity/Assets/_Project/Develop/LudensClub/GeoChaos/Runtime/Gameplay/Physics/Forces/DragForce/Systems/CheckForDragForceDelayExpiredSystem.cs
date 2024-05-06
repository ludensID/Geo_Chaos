using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CheckForDragForceDelayExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _delays;

    public CheckForDragForceDelayExpiredSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _delays = _physics
        .Filter<DragForceDelay>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delay in _delays
        .Where<DragForceDelay>(x => x.TimeLeft <= 0))
      {
        delay
          .Del<DragForceDelay>()
          .Add<Enabled>()
          .Replace((ref Gradient gradient) => gradient.Value = 0);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class CheckForDelayExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _delays;

    public CheckForDelayExpiredSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _delays = _physics
        .Filter<Delay>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity delay in _delays
        .Where<Delay>(x => x.TimeLeft <= 0))
      {
        delay
          .Del<Delay>()
          .Has<Prepared>(false)
          .Add<Enabled>()
          .Replace((ref Gradient gradient) => gradient.Value = 0);
      }
    }
  }
}
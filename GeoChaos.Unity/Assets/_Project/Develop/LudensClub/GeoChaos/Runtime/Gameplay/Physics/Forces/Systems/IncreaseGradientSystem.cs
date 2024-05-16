using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class IncreaseGradientSystem : IEcsRunSystem
  {
    private readonly EcsWorld _physics;
    private readonly EcsEntities _gradients;

    public IncreaseGradientSystem(PhysicsWorldWrapper physicsWorldWrapper)
    {
      _physics = physicsWorldWrapper.World;

      _gradients = _physics
        .Filter<Gradient>()
        .Inc<GradientRate>()
        .Inc<Enabled>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity grad in _gradients)
      {
        ref Gradient gradient = ref grad.Get<Gradient>();
        gradient.Value += grad.Get<GradientRate>().Rate * Time.fixedDeltaTime;
        gradient.Value = MathUtils.Clamp(gradient.Value, 0, 1);
      }
    }
  }
}
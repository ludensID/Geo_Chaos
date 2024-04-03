using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateHeroVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _vectors;

    public CalculateHeroVelocitySystem(GameWorldWrapper gameWorldWrapper)
    { 
      _world = gameWorldWrapper.World;

      _vectors = _world
        .Filter<HeroMovementVector>()
        .Inc<HeroVelocity>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int vector in _vectors)
      {
        ref HeroMovementVector movementVector = ref _world.Get<HeroMovementVector>(vector);
        ref HeroVelocity velocity = ref _world.Get<HeroVelocity>(vector);
        velocity.Velocity = movementVector.Direction * movementVector.Speed;
      }
    }
  }
}
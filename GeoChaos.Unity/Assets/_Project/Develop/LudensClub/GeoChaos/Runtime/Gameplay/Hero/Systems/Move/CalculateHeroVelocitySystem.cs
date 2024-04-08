using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
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
      foreach (var vector in _vectors)
      {
        ref var movementVector = ref _world.Get<HeroMovementVector>(vector);
        ref var velocity = ref _world.Get<HeroVelocity>(vector);
        velocity.Velocity = movementVector.Direction * movementVector.Speed;
      }
    }
  }
}
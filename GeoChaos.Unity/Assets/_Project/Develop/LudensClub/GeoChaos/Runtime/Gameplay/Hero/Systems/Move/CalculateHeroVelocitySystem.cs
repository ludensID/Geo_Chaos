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
        .Filter<MovementVector>()
        .Inc<Velocity>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var vector in _vectors)
      {
        ref var movementVector = ref _world.Get<MovementVector>(vector);
        ref var velocity = ref _world.Get<Velocity>(vector);
        velocity.Value = movementVector.Direction * movementVector.Speed;
      }
    }
  }
}
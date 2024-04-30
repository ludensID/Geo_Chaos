using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _vectors;

    public CalculateVelocitySystem(GameWorldWrapper gameWorldWrapper)
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
        ref MovementVector movementVector = ref _world.Get<MovementVector>(vector);
        ref Velocity velocity = ref _world.Get<Velocity>(vector);
        velocity.Value = movementVector.Direction * movementVector.Speed;
      }
    }
  }
}
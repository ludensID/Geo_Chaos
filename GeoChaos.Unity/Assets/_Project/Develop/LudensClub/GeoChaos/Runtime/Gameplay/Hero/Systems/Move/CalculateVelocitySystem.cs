using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CalculateVelocitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _vectors;

    public CalculateVelocitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _vectors = _world
        .Filter<MovementVector>()
        .Inc<Velocity>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        ref MovementVector movementVector = ref vector.Get<MovementVector>();
        ref Velocity velocity = ref vector.Get<Velocity>();
        velocity.Value = movementVector.Direction * movementVector.Speed;
      }
    }
  }
}
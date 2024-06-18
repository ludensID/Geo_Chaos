using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces
{
  public class AssignLastMovementVectorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _vectors;

    public AssignLastMovementVectorSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _vectors = _game
        .Filter<MovementVector>()
        .Inc<LastMovementVector>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity vector in _vectors)
      {
        ref MovementVector movementVector = ref vector.Get<MovementVector>();
        ref LastMovementVector lastMovementVector = ref vector.Get<LastMovementVector>();
        lastMovementVector.Speed = movementVector.Speed;
        lastMovementVector.Direction = movementVector.Direction;
      }
    }
  }
}
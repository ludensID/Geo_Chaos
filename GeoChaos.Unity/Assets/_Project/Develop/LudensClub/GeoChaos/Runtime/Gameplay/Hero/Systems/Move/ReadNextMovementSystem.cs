using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadNextMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _movements;

    public ReadNextMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _movements = _world
        .Filter<MovementQueue>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int movement in _movements)
      {
        ref MovementQueue queue = ref _world.Get<MovementQueue>(movement);
        
        if (queue.NextMovements.Count > 0)
        {
          DelayedMovement next = queue.NextMovements[0];
          if (next.WaitingTime <= 0)
          {
            ref MoveCommand command = ref _world.Add<MoveCommand>(movement);
            command.Direction = next.Direction;
            
            queue.NextMovements.Remove(next);
          }
        }
      }
    }
  }
}
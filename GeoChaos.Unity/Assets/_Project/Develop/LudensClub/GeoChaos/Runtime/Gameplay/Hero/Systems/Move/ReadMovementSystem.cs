using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class ReadMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _movables;
    private readonly EcsWorld _inputWorld;
    private readonly EcsFilter _inputs;

    public ReadMovementSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _inputWorld = inputWorldWrapper.World;

      _movables = _world
        .Filter<Movable>()
        .End();

      _inputs = _inputWorld
        .Filter<Expired>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int input in _inputs)
        foreach (int movable in _movables)
        {
          ref HorizontalMovement horizontalMovement = ref _inputWorld.Get<HorizontalMovement>(input);
          ref MoveCommand command = ref _world.Add<MoveCommand>(movable);
          command.Direction = horizontalMovement.Direction;
        }
    }
  }
}
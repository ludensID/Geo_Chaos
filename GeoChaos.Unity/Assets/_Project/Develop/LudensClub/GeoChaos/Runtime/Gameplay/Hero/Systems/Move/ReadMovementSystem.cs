using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
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
        .Exc<IsMovementLocked>()
        .End();

      _inputs = _inputWorld
        .Filter<Expired>()
        .Inc<HorizontalMovement>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var input in _inputs)
      foreach (var movable in _movables)
      {
        ref var horizontalMovement = ref _inputWorld.Get<HorizontalMovement>(input);
        ref var command = ref _world.Add<MoveCommand>(movable);
        command.Direction = horizontalMovement.Direction;
      }
    }
  }
}
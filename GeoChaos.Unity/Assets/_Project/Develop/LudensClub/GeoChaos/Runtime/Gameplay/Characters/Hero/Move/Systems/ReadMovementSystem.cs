using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Move
{
  public class ReadMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsWorld _inputWorld;
    private readonly EcsEntities _movables;
    private readonly EcsEntities _inputs;

    public ReadMovementSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _inputWorld = inputWorldWrapper.World;

      _movables = _world
        .Filter<HeroTag>()
        .Inc<Movable>()
        .Collect();

      _inputs = _inputWorld
        .Filter<Expired>()
        .Inc<HorizontalMovement>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity movable in _movables)
      {
        float direction = input.Get<HorizontalMovement>().Direction;
        if (direction != 0)
          movable.Add((ref MoveHeroCommand command) => command.Direction = direction);
      }
    }
  }
}
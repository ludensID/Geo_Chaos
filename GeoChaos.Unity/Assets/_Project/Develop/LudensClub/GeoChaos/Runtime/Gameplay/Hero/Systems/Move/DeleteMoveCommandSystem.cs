using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class DeleteMoveCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _commands;

    public DeleteMoveCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _commands = _world
        .Filter<MoveCommand>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int command in _commands)
      {
        _world.Del<MoveCommand>(command);
      }
    }
  }
}
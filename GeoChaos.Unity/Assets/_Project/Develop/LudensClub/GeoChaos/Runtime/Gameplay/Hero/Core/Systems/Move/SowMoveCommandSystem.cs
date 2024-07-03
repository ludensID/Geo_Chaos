using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class SowMoveCommandSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _commands;

    public SowMoveCommandSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _commands = _game
        .Filter<MoveCommand>()
        .Exc<FreeFalling>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity command in _commands
        .Where<MovementLayout>(x => x.Layer != MovementLayer.All))
      {
        command.Del<MoveCommand>();
      }
    }
  }
}
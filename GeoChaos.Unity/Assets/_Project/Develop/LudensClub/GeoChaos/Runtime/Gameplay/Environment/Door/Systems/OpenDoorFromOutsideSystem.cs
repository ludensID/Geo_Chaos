using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class OpenDoorFromOutsideSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _openDoorCommands;

    public OpenDoorFromOutsideSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _openDoorCommands = _game
        .Filter<DoorTag>()
        .Inc<OpenCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _openDoorCommands)
      {
        door
          .Del<OpenCommand>()
          .Del<Closed>()
          .Add<Opened>();
      }
    }
  }
}
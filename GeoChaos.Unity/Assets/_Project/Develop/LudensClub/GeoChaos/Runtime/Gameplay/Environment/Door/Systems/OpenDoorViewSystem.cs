using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class OpenDoorViewSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _openedDoors;

    public OpenDoorViewSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      
      _openedDoors = _game
        .Filter<DoorTag>()
        .Inc<Opened>()
        .Inc<ColliderRef>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _openedDoors)
      {
        door.Get<ColliderRef>().Collider.enabled = false;
      }
    }
  }
}
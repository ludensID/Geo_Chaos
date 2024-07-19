using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class OpenDoorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _openedDoors;

    public OpenDoorSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _openedDoors = _game
        .Filter<DoorTag>()
        .Inc<OpenCommand>()
        .Inc<OnInteracted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _openedDoors)
      {
        door
          .Del<OnInteracted>()
          .Del<CanInteract>()
          .Del<Interactable>()
          .Del<OpenCommand>()
          .Del<Closed>()
          .Add<Opened>()
          .Add<OnOpened>();

        if (door.Get<MatchedKeyRef>().Key.Entity.TryUnpackEntity(_game, out EcsEntity key))
          key.Add<DestroyCommand>();
      }
    }
  }
}
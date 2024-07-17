using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class SendBlankDoorInteractionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _message;
    private readonly EcsEntities _interactedDoors;

    public SendBlankDoorInteractionSystem(GameWorldWrapper gameWorldWrapper, MessageWorldWrapper messageWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _message = messageWorldWrapper.World;

      _interactedDoors = _game
        .Filter<DoorTag>()
        .Inc<OnInteracted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _interactedDoors)
      {
        _message.CreateEntity()
          .Add<NothingHappensMessage>();

        door.Del<OnInteracted>();
      }
    }
  }
}
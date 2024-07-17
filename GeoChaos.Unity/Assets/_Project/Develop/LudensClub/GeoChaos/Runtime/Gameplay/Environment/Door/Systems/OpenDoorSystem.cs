using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class OpenDoorSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _matchedKeys;

    public OpenDoorSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _matchedKeys = _game
        .Filter<KeyTag>()
        .Inc<MatchedDoor>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity key in _matchedKeys)
      {
        if (key.Get<MatchedDoor>().Door.TryUnpackEntity(_game, out EcsEntity door))
        {
          door
            .Del<OnInteracted>()
            .Del<Closed>()
            .Add<Opened>();

          key.Add<DestroyCommand>();
        }
      }
    }
  }
}
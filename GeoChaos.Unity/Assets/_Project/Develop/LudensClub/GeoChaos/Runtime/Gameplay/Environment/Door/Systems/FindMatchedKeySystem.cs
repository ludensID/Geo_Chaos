using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.DoorKey;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class FindMatchedKeySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interactedDoors;
    private readonly EcsEntities _ownedKeys;

    public FindMatchedKeySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _interactedDoors = _game
        .Filter<DoorTag>()
        .Inc<OnInteracted>()
        .Collect();

      _ownedKeys = _game
        .Filter<KeyTag>()
        .Inc<Owner>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _interactedDoors)
      {
        BaseView keyView = door.Get<KeyViewRef>().Key;
        foreach (EcsEntity key in _ownedKeys)
        {
          if (key.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity hero)
            && hero.Has<HeroTag>()
            && keyView == key.Get<ViewRef>().View)
          {
            key.Add((ref MatchedDoor matchedDoor) => matchedDoor.Door = door.Pack());
          }
        }
      }
    }
  }
}
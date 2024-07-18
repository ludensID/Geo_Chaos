using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  public class FindMatchedKeySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _interactedDoors;

    public FindMatchedKeySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _interactedDoors = _game
        .Filter<DoorTag>()
        .Inc<OnInteracted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity door in _interactedDoors)
      {
        BaseView keyView = door.Get<MatchedKeyRef>().Key;
        if (keyView && keyView.Entity.TryUnpackEntity(_game, out EcsEntity key)
          && key.Has<Owner>()
          && key.Get<Owner>().Entity.TryUnpackEntity(_game, out EcsEntity hero)
          && hero.Has<HeroTag>())
        {
          key.Add((ref MatchedDoor matchedDoor) => matchedDoor.Door = door.Pack());
        }
      }
    }
  }
}

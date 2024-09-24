using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation
{
  public class MoveToSpawnEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _convertedEntities;
    private readonly EcsEntity _spawn;

    public MoveToSpawnEntitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _convertedEntities = _game
        .Filter<OnConverted>()
        .Inc<Spawned>()
        .Inc<ViewRef>()
        .Collect();

      _spawn = new EcsEntity(_game);
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity converted in _convertedEntities)
      {
        ref Spawned spawned = ref converted.Get<Spawned>();
        if (spawned.Spawn.TryUnpackEntity(_game, _spawn))
        {
          Transform transform = converted.Get<ViewRef>().View.transform;
          Transform spawnTransform = _spawn.Get<ViewRef>().View.transform;
          transform.SetPositionAndRotation(spawnTransform.position, spawnTransform.rotation);
        }
      }
    }
  }
}
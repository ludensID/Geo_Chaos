using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class MoveToSpawnEntitySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _spawnables;

    public MoveToSpawnEntitySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _spawnables = _game
        .Filter<SpawnAvailable>()
        .Inc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spawnable in _spawnables)
      {
        ref SpawnAvailable spawn = ref spawnable.Get<SpawnAvailable>();
        Transform transform = spawnable.Get<ViewRef>().View.transform;
        transform.position = spawn.Position;
        transform.rotation = spawn.Rotation;

        spawnable.Del<SpawnAvailable>();
      }
    }
  }
}
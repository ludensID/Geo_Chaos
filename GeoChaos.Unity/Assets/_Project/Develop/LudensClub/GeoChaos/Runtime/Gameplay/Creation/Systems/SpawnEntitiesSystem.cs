using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class SpawnEntitiesSystem : IEcsInitSystem
  {
    private readonly List<SpawnPoint> _spawns;
    private readonly EcsWorld _game;
    private readonly PrefabProvider _prefabs;

    public SpawnEntitiesSystem(GameWorldWrapper gameWorldWrapper, List<SpawnPoint> spawns,
      IConfigProvider configProvider)
    {
      _spawns = spawns;
      _game = gameWorldWrapper.World;
      _prefabs = configProvider.Get<PrefabProvider>();
    }

    public void Init(EcsSystems systems)
    {
      foreach (SpawnPoint spawn in _spawns)
      {
        _game.CreateEntity()
          .Add<CreateCommand>()
          .Add((ref EntityId id) => id.Id = spawn.EntityId)
          .Add((ref ViewPrefab prefab) => prefab.Prefab = _prefabs.Get(spawn.EntityId))
          .Add((ref SpawnAvailable spawnAvailable) =>
          {
            spawnAvailable.Position = spawn.transform.position;
            spawnAvailable.Rotation = spawn.transform.rotation;
          })
          .Add((ref SpawnPointRef spawnPoint) => spawnPoint.Spawn = spawn);
      }
    }
  }
}
using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class SpawnEntitiesSystem : IEcsInitSystem
  {
    private readonly List<SpawnPoint> _spawns;
    private readonly EcsWorld _game;
    private readonly PrefabConfig _prefabs;

    public SpawnEntitiesSystem(GameWorldWrapper gameWorldWrapper, List<SpawnPoint> spawns,
      IConfigProvider configProvider)
    {
      _spawns = spawns;
      _game = gameWorldWrapper.World;
      _prefabs = configProvider.Get<PrefabConfig>();
    }

    public void Init(EcsSystems systems)
    {
      foreach (var spawn in _spawns)
      {
        var entity = _game.NewEntity();
        _game.Add<CreateCommand>(entity);
        ref var id = ref _game.Add<EntityId>(entity);
        id.Id = spawn.EntityId;

        ref var prefab = ref _game.Add<ViewPrefab>(entity);
        prefab.Prefab = _prefabs.Get(spawn.EntityId);

        ref var spawnAvailable = ref _game.Add<SpawnAvailable>(entity);
        spawnAvailable.Position = spawn.transform.position;
        spawnAvailable.Rotation = spawn.transform.rotation;
      }
    }
  }
}
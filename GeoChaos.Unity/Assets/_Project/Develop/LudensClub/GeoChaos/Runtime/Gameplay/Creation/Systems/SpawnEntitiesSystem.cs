using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemies;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Props;
using LudensClub.GeoChaos.Runtime.Props.Enemies.Lama;
using LudensClub.GeoChaos.Runtime.Utils;

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
        EcsEntity instance = _game.CreateEntity()
          .Add<CreateCommand>()
          .Add((ref EntityId id) => id.Id = spawn.EntityId)
          .Add((ref ViewPrefab prefab) => prefab.Prefab = _prefabs.Get(spawn.EntityId))
          .Add((ref SpawnAvailable spawnAvailable) =>
          {
            spawnAvailable.Position = spawn.transform.position;
            spawnAvailable.Rotation = spawn.transform.rotation;
          });

        if (spawn.EntityId.IsEnemy())
          instance.Add((ref BrainContext ctx) => ctx.Context = spawn.GetComponent<BrainContextView>()?.Context);
      }
    }
  }
}
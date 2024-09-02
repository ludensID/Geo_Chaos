using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.SpawnPoint
{
  public class SpawnSystem : IEcsRunSystem
  {
    private readonly IViewFactory _factory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _spawns;
    private readonly EcsEntity _spawnedEntity;

    public SpawnSystem(GameWorldWrapper gameWorldWrapper, IViewFactory factory)
    {
      _factory = factory;
      _game = gameWorldWrapper.World;

      _spawns = _game
        .Filter<SpawnPointTag>()
        .Inc<Spawnable>()
        .Collect();

      _spawnedEntity = new EcsEntity(_game, -1);
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spawn in _spawns)
      {
        GameObjectConverter instance = _factory.Create<GameObjectConverter>(spawn.Get<SpawnedEntityId>().Id);
        _spawnedEntity.Entity = _game.NewEntity();
        instance.SetEntity(_game, _spawnedEntity.Entity);

        _spawnedEntity
          .Add((ref Spawned spawned) => spawned.Spawn = spawn.PackedEntity);

        spawn.Del<Spawnable>();
      }
    }
  }
}
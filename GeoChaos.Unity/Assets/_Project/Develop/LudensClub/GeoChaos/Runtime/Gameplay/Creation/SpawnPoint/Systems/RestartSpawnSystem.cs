using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Restart;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.SpawnPoint
{
  public class RestartSpawnSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _restartMessages;
    private readonly EcsEntities _spawnPoints;
    private readonly EcsEntities _spawnedEntities;

    public RestartSpawnSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _restartMessages = _message
        .Filter<AfterRestartMessage>()
        .Collect();

      _spawnPoints = _game
        .Filter<SpawnPointTag>()
        .Exc<Spawnable>()
        .Collect();

      _spawnedEntities = _game
        .Filter<Spawned>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _restartMessages)
      {
        foreach (EcsEntity spawnPoint in _spawnPoints)
          spawnPoint.Add<Spawnable>();

        foreach (EcsEntity spawned in _spawnedEntities)
        {
          if (spawned.Get<Spawned>().Spawn.TryUnpackEntity(_game, out EcsEntity spawn))
            spawn.Del<Spawnable>();
        }
      }
    }
  }
}
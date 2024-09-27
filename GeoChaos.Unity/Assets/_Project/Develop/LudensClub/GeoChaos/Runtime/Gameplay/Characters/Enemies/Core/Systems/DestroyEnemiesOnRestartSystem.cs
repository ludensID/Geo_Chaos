using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Restart;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class DestroyEnemiesOnRestartSystem : IEcsRunSystem
  {
    private readonly EcsWorld _message;
    private readonly EcsWorld _game;
    private readonly EcsEntities _restartMessage;
    private readonly EcsEntities _spawnedEnemies;

    public DestroyEnemiesOnRestartSystem(MessageWorldWrapper messageWorldWrapper, GameWorldWrapper gameWorldWrapper)
    {
      _message = messageWorldWrapper.World;
      _game = gameWorldWrapper.World;

      _restartMessage = _message
        .Filter<OnRestartMessage>()
        .Collect();

      _spawnedEnemies = _game
        .Filter<EnemyTag>()
        .Inc<Spawned>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _restartMessage)
      foreach (EcsEntity enemy in _spawnedEnemies)
      {
        enemy.Add<DestroyCommand>();
      }
    }
  }
}
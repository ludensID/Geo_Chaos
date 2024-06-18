using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class DeleteInitializeCommandForEnemySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _enemies;

    public DeleteInitializeCommandForEnemySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<InitializeCommand>()
        .Inc<EnemyTag>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _enemies) 
        enemy.Del<InitializeCommand>();
    }
  }
}
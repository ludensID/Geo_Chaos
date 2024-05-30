using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class SelectNearestEnemySystem : IEcsRunSystem
  {
    private readonly EnemySelector _selector;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _enemies;
    private readonly EcsEntities _selectedEnemies;

    public SelectNearestEnemySystem(GameWorldWrapper gameWorldWrapper, EnemySelector selector)
    {
      _selector = selector;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _enemies = _game
        .Filter<EnemyTag>()
        .Collect();

      _selectedEnemies = _game
        .Filter<EnemyTag>()
        .Inc<Selected>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      _selector.Select(_heroes, _enemies, _selectedEnemies);
    }
  }
}
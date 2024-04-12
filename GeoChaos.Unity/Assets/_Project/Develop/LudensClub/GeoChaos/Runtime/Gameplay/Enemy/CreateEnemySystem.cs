using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Enemy;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Enemy
{
  public class CreateEnemySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _enemies;

    public CreateEnemySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<CreateCommand>()
        .Inc<EntityId>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var enemy in _enemies
        .Where((ref EntityId x) => x.Id == EntityType.Enemy))
      {
        _game.Add<EnemyTag>(enemy);

        ref var health = ref _game.Add<Health>(enemy);
        health.Value = 100;

        _game.Del<CreateCommand>(enemy);
        _game.Add<InitializeCommand>(enemy);
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Characteristics.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class CreateEnemySystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _enemies;

    public CreateEnemySystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _enemies = _game
        .Filter<CreateCommand>()
        .Inc<EntityId>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _enemies
        .Where<EntityId>(x => x.Id.IsEnemy()))
      {
        enemy
          .Add<EnemyTag>()
          .Add((ref Health x) => x.Value = 100)
          .Del<CreateCommand>()
          .Add<InitializeCommand>()
          .Add<Brain>();
      }
    }
  }
}
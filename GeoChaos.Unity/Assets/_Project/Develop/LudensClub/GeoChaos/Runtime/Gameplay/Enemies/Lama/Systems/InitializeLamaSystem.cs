using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Attack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class InitializeLamaSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _initializedEnemies;

    public InitializeLamaSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _initializedEnemies = _game
        .Filter<InitializeCommand>()
        .Inc<EnemyTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _initializedEnemies
        .Where<EntityId>(x => x.Id == EntityType.Lama))
      {
        enemy
          .Add<LamaTag>()
          .Add<MovementVector>()
          .Add((ref BodyDirection bodyDirection) => bodyDirection.Direction = 1)
          .Add<ForceAvailable>()
          .Add<PatrolBounds>()
          .Add<ComboAttackCounter>();
      }
    }
  }
}
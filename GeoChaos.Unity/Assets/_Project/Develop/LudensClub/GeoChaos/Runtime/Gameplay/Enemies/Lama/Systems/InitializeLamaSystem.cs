using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
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
          .Add<BodyDirection>()
          .Add<ForceAvailable>()
          .Add<PhysicalBounds>()
          .Add<PatrolBounds>()
          .Add<ChasingBounds>();
      }
    }
  }
}
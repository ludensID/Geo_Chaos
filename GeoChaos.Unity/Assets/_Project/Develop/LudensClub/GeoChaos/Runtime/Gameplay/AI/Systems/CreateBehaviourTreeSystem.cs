using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class CreateBehaviourTreeSystem : IEcsRunSystem
  {
    private readonly ITreeCreatorService _creator;
    private readonly EcsWorld _game;
    private readonly EcsEntities _initializingEnemies;

    public CreateBehaviourTreeSystem(GameWorldWrapper gameWorldWrapper, ITreeCreatorService creator)
    {
      _creator = creator;
      _game = gameWorldWrapper.World;

      _initializingEnemies = _game
        .Filter<OnConverted>()
        .Inc<Brain>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity enemy in _initializingEnemies)
      {
        enemy.Change((ref Brain brain) => brain.Tree = _creator.Create(enemy.Get<EntityId>().Id, enemy.Pack()));
      }
    }
  }
}
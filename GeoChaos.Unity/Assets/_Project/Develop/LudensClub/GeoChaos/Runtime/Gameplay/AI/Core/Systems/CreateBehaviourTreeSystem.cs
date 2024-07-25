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
    private readonly EcsEntities _convertedBrains;

    public CreateBehaviourTreeSystem(GameWorldWrapper gameWorldWrapper, ITreeCreatorService creator)
    {
      _creator = creator;
      _game = gameWorldWrapper.World;

      _convertedBrains = _game
        .Filter<OnConverted>()
        .Inc<Brain>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity brain in _convertedBrains)
      {
        brain.Change((ref Brain b) => b.Tree = _creator.Create(brain.Get<EntityId>().Id, brain.Pack()));
      }
    }
  }
}
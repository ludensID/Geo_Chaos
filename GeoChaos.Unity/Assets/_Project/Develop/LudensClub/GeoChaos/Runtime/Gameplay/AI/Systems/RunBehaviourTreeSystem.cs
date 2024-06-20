using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class RunBehaviourTreeSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _brains;

    public RunBehaviourTreeSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _brains = _game
        .Filter<Brain>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity brain in _brains)
      {
        brain.Get<Brain>().Tree.Run();
      }
    }
  }
}
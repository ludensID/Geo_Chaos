using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class OpenCheckpointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _checkpoints;

    public OpenCheckpointSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _checkpoints = _game
        .Filter<CheckpointTag>()
        .Inc<Closed>()
        .Inc<OnInteracted>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity checkpoint in _checkpoints)
      {
        checkpoint
          .Del<Closed>()
          .Add<Opened>()
          .Add<OnOpened>();
      }
    }
  }
}
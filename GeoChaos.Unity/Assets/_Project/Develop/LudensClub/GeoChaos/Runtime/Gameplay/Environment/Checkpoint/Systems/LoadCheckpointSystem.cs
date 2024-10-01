using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Persistence;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class LoadCheckpointSystem : IEcsRunSystem
  {
    private readonly IPersistenceService _persistence;
    private readonly EcsWorld _game;
    private readonly EcsEntities _convertedCheckpoints;

    public LoadCheckpointSystem(GameWorldWrapper gameWorldWrapper, IPersistenceService persistence)
    {
      _persistence = persistence;
      _game = gameWorldWrapper.World;

      _convertedCheckpoints = _game
        .Filter<CheckpointTag>()
        .Inc<OnConverted>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity checkpoint in _convertedCheckpoints)
      {
        if (_persistence.Data.OpenedCheckpoints.Contains(checkpoint.Get<PersistenceIdRef>().Identifier.Id))
        {
          checkpoint
            .Del<Closed>()
            .Add<Opened>();
        }
      }
    }
  }
}
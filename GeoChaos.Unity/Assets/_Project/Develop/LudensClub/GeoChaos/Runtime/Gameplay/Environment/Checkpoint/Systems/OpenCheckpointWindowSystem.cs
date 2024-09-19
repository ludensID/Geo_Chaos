using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Interaction;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Windows;
using LudensClub.GeoChaos.Runtime.Windows.Map;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class OpenCheckpointWindowSystem : IEcsRunSystem
  {
    private readonly IWindowManager _windowManager;
    private readonly MapModel _mapModel;
    private readonly EcsWorld _game;
    private readonly EcsEntities _openedCheckpoints;

    public OpenCheckpointWindowSystem(GameWorldWrapper gameWorldWrapper, IWindowManager windowManager, MapModel mapModel)
    {
      _windowManager = windowManager;
      _mapModel = mapModel;
      _game = gameWorldWrapper.World;

      _openedCheckpoints = _game
        .Filter<CheckpointTag>()
        .Inc<OnInteracted>()
        .Inc<Opened>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity checkpoint in _openedCheckpoints)
      {
        _mapModel.CurrentCheckpoint.Copy(checkpoint);
        _windowManager.Open(WindowType.Checkpoint);
      }
    }
  }
}
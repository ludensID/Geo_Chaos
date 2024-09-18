using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Windows;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  public class OpenCheckpointWindowSystem : IEcsRunSystem
  {
    private readonly IWindowManager _windowManager;
    private readonly EcsWorld _game;
    private readonly EcsEntities _openedCheckpoints;

    public OpenCheckpointWindowSystem(GameWorldWrapper gameWorldWrapper, IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _game = gameWorldWrapper.World;

      _openedCheckpoints = _game
        .Filter<CheckpointTag>()
        .Inc<OnInteracted>()
        .Inc<Opened>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      if (_openedCheckpoints.Any())
        _windowManager.Open(WindowType.Checkpoint);
    }
  }
}
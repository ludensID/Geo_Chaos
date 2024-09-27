using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment;
using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  public class MoveButtonPresenter : IMoveButtonPresenter, IInitializable, ITickable, IDisposable
  {
    private readonly IWindowManager _windowManager;
    private readonly IExplicitInitializer _initializer;
    private MoveButtonView _view;
    private readonly EcsWorld _game;
    private readonly EcsEntities _openedCheckpoints;

    public MoveButtonPresenter(IWindowManager windowManager,
      GameWorldWrapper gameWorldWrapper,
      IExplicitInitializer initializer)
    {
      _windowManager = windowManager;
      _initializer = initializer;
      _initializer.Add(this);
      _game = gameWorldWrapper.World;

      _openedCheckpoints = _game
        .Filter<CheckpointTag>()
        .Inc<Opened>()
        .Collect();
    }

    public void SetView(MoveButtonView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _view.SetActiveButton(false);
    }

    public void Tick()
    {
      if (_openedCheckpoints.Filter.GetEntitiesCount() > 1)
      {
        _view.SetActiveButton(true);
      }
    }

    public void OpenMap()
    {
      _windowManager.Open(WindowType.Map);
    }

    public void Dispose()
    {
      _initializer.Remove(this);
    }
  }
}
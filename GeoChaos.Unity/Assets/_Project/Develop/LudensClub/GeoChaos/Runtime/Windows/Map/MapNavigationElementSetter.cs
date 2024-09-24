using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  public class MapNavigationElementSetter : IMapNavigationElementSetter, IInitializable, IDisposable
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly EventSystem _eventSystem;
    private readonly IWindowManager _windowManager;
    private readonly MapModel _model;

    private List<MapCheckpointButtonView> _children;
    private MapWindowView _view;
    private WindowController _mapWindow;

    public MapNavigationElementSetter(IWindowManager windowManager, MapModel model, IExplicitInitializer initializer)
    {
      _windowManager = windowManager;
      _model = model;
      initializer.Add(this);
    }

    public void SetView(MapWindowView view)
    {
      _view = view;
      _children = _view.GetComponentsInChildren<MapCheckpointButtonView>().ToList();
    }

    public void Initialize()
    {
      _mapWindow = (WindowController)_windowManager.FindWindowById(WindowType.Map);
      _mapWindow.OnBeforeOpen += SelectInteractedCheckpoint;
    }

    public void Dispose()
    {
      _mapWindow.OnBeforeOpen -= SelectInteractedCheckpoint;
    }

    private void SelectInteractedCheckpoint()
    {
      foreach (MapCheckpointButtonView child in _children)
      {
        if (child.Checkpoint.Entity.EqualsTo(_model.CurrentCheckpoint.PackedEntity))
        {
          _mapWindow.Model.FirstNavigationElement = child.gameObject;
        }
      }
    }
  }
}
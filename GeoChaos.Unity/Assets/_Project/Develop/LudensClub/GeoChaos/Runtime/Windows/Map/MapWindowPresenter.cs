using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  public class MapWindowPresenter : IMapWindowPresenter, IInitializable
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly EventSystem _eventSystem;
    private readonly MapModel _model;
    
    private List<MapCheckpointButtonView> _children;
    private MapWindowView _view;

    public WindowType Id => WindowType.Map;
    public bool IsOpened { get; private set; }

    public MapWindowPresenter(LevelStateMachine levelStateMachine,
      IWindowManager windowManager,
      IExplicitInitializer initializer,
      EventSystem eventSystem,
      MapModel model)
    {
      _levelStateMachine = levelStateMachine;
      _eventSystem = eventSystem;
      _model = model;
      windowManager.Add(this);
      initializer.Add(this);
    }

    public void SetView(MapWindowView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _children = _view.GetComponentsInChildren<MapCheckpointButtonView>().ToList();

      _view.gameObject.SetActive(false);
      IsOpened = false;
    }

    public void Open()
    {
      if (!IsOpened)
      {
        _levelStateMachine.SwitchState<PauseLevelState>().Forget();
        SelectInteractedCheckpoint();
        
        _view.gameObject.SetActive(true);
        IsOpened = true;
      }
    }

    private void SelectInteractedCheckpoint()
    {
      foreach (MapCheckpointButtonView child in _children)
      {
        if (child.Checkpoint.Entity.EqualsTo(_model.CurrentCheckpoint.PackedEntity))
        {
          _eventSystem.SetSelectedGameObject(child.gameObject);
        }
      }
    }

    public void Close()
    {
      if (IsOpened)
      {
        _view.gameObject.SetActive(false);
        _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
        IsOpened = false;
        _eventSystem.SetSelectedGameObject(null);
      }
    }
  }
}
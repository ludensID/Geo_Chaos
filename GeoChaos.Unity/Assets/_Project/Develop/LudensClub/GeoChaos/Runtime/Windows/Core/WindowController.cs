using System;
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowController : IWindowController, IInitializable, ICloseHandler
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly EventSystem _eventSystem;
    private readonly WindowModel _model;

    private WindowView _view;

    public WindowType Id => _view.Id;
    public bool IsOpened { get; private set; }

    public WindowModel Model => _model;

    public event Action OnBeforeOpen;
    public event Action OnBeforeClose;

    public WindowController(LevelStateMachine levelStateMachine,
      IWindowManager windowManager,
      IExplicitInitializer initializer,
      EventSystem eventSystem,
      WindowModel model)
    {
      _levelStateMachine = levelStateMachine;
      _eventSystem = eventSystem;
      _model = model;

      initializer.Add(this);
      windowManager.Add(this);
    }

    public void SetView(WindowView view)
    {
      _view = view;
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
      IsOpened = false;
    }

    public void Open()
    {
      if (!IsOpened)
      {
        OnBeforeOpen?.Invoke();
        _levelStateMachine.SwitchState<PauseLevelState>().Forget();
        _view.gameObject.SetActive(true);
        IsOpened = true;
        _eventSystem.SetSelectedGameObject(_model.FirstNavigationElement);
      }
    }

    public void Close()
    {
      if (IsOpened)
      {
        OnBeforeClose?.Invoke();
        _view.gameObject.SetActive(false);
        IsOpened = false;
        _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
        _eventSystem.SetSelectedGameObject(null);
      }
    }
  }
}
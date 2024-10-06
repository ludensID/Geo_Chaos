using System;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  public class WindowController : IWindowController, IInitializable, ICloseHandler
  {
    private readonly EventSystem _eventSystem;
    private readonly WindowModel _model;

    private WindowView _view;

    public WindowType Id => _view.Id;
    public bool IsOpened { get; private set; }

    public WindowModel Model => _model;

    public event Action OnBeforeOpen;
    public event Action OnBeforeClose;
    public event Action OnClosed;

    public WindowController(IWindowManager windowManager,
      IExplicitInitializer initializer,
      EventSystem eventSystem,
      WindowModel model)
    {
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
      if (!IsOpened)
        _view.gameObject.SetActive(false);
    }

    public void Open()
    {
      if (!IsOpened)
      {
        OnBeforeOpen?.Invoke();
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
        _eventSystem.SetSelectedGameObject(null);
        OnClosed?.Invoke();
      }
    }
  }
}
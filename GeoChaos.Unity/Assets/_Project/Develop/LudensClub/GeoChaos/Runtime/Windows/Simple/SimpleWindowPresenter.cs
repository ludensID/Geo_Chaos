using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  public class SimpleWindowPresenter : ISimpleWindowPresenter, IInitializable
  {
    private readonly IGameplayPause _pause;
    private readonly EventSystem _eventSystem;
    private SimpleWindowView _view;

    public WindowType Id => _view.Id;
    public bool IsOpened { get; private set; }

    public SimpleWindowPresenter(IGameplayPause pause,
      IWindowManager windowManager,
      IExplicitInitializer initializer,
      EventSystem eventSystem)
    {
      _pause = pause;
      _eventSystem = eventSystem;
      windowManager.Add(this);
      initializer.Add(this);
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
      IsOpened = false;
    }

    public void SetView(SimpleWindowView view)
    {
      _view = view;
    }

    public void Open()
    {
      if (!IsOpened)
      {
        _pause.SetPause(true);
        _view.gameObject.SetActive(true);
        IsOpened = true;
        _eventSystem.SetSelectedGameObject(_view.FirstNavigationElement.gameObject);
      }
    }

    public void Close()
    {
      if (IsOpened)
      {
        _view.gameObject.SetActive(false);
        _pause.SetPause(false);
        IsOpened = false;
        _eventSystem.SetSelectedGameObject(null);
      }
    }
  }
}
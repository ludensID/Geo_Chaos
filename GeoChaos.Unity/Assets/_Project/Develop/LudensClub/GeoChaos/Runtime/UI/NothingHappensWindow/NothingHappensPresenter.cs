using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Windows;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  public class NothingHappensPresenter : INothingHappensPresenter, IInitializable
  {
    private readonly IGameplayPause _pause;
    private readonly IWindowManager _windowManager;
    private NothingHappensView _view;

    public WindowType Id => WindowType.NothingHappens;
    public bool IsOpened { get; private set; }

    public NothingHappensPresenter(IGameplayPause pause, IWindowManager windowManager, InitializableManager initializer)
    {
      _pause = pause;
      _windowManager = windowManager;
      windowManager.Add(this);
      initializer.Add(this);
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
      IsOpened = false;
    }

    public void SetView(NothingHappensView view)
    {
      _view = view;
    }

    public void CloseItself()
    {
      if (IsOpened)
        _windowManager.Close();
    }

    public void Open()
    {
      if (!IsOpened)
      {
        _pause.SetPause(true);
        _view.gameObject.SetActive(true);
        IsOpened = true;
      }
    }

    public void Close()
    {
      if (IsOpened)
      {
        _view.gameObject.SetActive(false);
        _pause.SetPause(false);
        IsOpened = false;
      }
    }
  }
}
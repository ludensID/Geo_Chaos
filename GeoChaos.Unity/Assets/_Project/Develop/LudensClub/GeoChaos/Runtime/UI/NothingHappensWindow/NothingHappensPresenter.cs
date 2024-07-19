using LudensClub.GeoChaos.Runtime.Infrastructure;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  public class NothingHappensPresenter : INothingHappensPresenter, IInitializable
  {
    private readonly IGameplayPause _pause;
    private NothingHappensView _view;

    public bool IsShown { get; private set; }

    public NothingHappensPresenter(IGameplayPause pause)
    {
      _pause = pause;
    }

    public void Initialize()
    {
      _view.gameObject.SetActive(false);
      IsShown = false;
    }

    public void SetView(NothingHappensView view)
    {
      _view = view;
    }

    public void ShowWindow()
    {
      if (!IsShown)
      {
        _pause.SetPause(true);
        _view.gameObject.SetActive(true);
        IsShown = true;
      }
    }

    public void CloseWindow()
    {
      if(IsShown)
      {
        _view.gameObject.SetActive(false);
        _pause.SetPause(false);
        IsShown = false;
      }
    }
  }
}
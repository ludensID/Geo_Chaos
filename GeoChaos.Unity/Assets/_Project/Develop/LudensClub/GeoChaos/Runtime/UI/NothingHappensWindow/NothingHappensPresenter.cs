using Zenject;

namespace LudensClub.GeoChaos.Runtime.UI.NothingHappensWindow
{
  public class NothingHappensPresenter : INothingHappensPresenter, IInitializable
  {
    private NothingHappensView _view;

    public bool IsShown { get; private set; }

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
        _view.gameObject.SetActive(true);
        IsShown = true;
      }
    }

    public void CloseWindow()
    {
      if(IsShown)
      {
        _view.gameObject.SetActive(false);
        IsShown = false;
      }
    }
  }
}
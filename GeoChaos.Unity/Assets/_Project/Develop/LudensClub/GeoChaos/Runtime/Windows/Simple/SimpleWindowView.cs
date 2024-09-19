using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  [AddComponentMenu(ACC.Names.SIMPLE_WINDOW_VIEW)]
  public class SimpleWindowView : BaseWindowView, ICancelHandler
  {
    public WindowType Id;
    public RectTransform FirstNavigationElement;
    
    private ISimpleWindowPresenter _presenter;
    private IWindowManager _windowManager;

    [Inject]
    public void Construct(ISimpleWindowPresenter presenter, IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _presenter = presenter;
      _presenter.SetView(this);
    }

    public void OnCancel(BaseEventData eventData)
    {
      Debug.Log(eventData.GetType().Name);
      _windowManager.Close(Id);
    }
  }
}
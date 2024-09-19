using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  [AddComponentMenu(ACC.Names.SIMPLE_WINDOW_VIEW)]
  public class SimpleWindowView : BaseWindowView
  {
    public WindowType Id;
    public RectTransform FirstNavigationElement;
    
    private ISimpleWindowPresenter _presenter;

    [Inject]
    public void Construct(ISimpleWindowPresenter presenter)
    {
      _presenter = presenter;
      _presenter.SetView(this);
    }
  }
}
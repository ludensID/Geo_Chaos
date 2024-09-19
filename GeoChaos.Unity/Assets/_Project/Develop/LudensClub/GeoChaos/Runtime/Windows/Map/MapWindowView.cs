using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  [AddComponentMenu(ACC.Names.MAP_WINDOW_VIEW)]
  public class MapWindowView : BaseWindowView
  {
    [Inject]
    public void Construct(IMapWindowPresenter presenter)
    {
      presenter.SetView(this);
    }
  }
}
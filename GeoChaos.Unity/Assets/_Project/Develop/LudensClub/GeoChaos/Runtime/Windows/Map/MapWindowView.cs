using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Map
{
  [AddComponentMenu(ACC.Names.MAP_WINDOW_VIEW)]
  public class MapWindowView : WindowView
  {
    [Inject]
    public void Construct(IMapNavigationElementSetter presenter)
    {
      presenter.SetView(this);
    }

    private void OnValidate()
    {
      Id = WindowType.Map;
    }
  }
}
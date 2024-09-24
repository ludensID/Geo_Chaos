using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Simple
{
  [AddComponentMenu(ACC.Names.SIMPLE_WINDOW_VIEW)]
  public class SimpleWindowView : WindowView
  {
    public GameObject FirstNavigationElement;
    
    [Inject]
    public void Construct(INavigationElementSetter setter)
    {
      setter.SetView(this);
    }
  }
}
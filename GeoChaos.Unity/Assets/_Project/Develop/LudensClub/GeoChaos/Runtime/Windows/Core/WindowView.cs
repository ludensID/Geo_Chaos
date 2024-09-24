using UnityEngine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  [AddComponentMenu(ACC.Names.WINDOW_VIEW)]
  [SelectionBase]
  public class WindowView : MonoBehaviour
  {
    public WindowType Id;
    
    [Inject]
    public void Construct(WindowController controller)
    {
      controller.SetView(this);
    }
  }
}
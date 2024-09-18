using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows
{
  [AddComponentMenu(ACC.Names.CLOSE_BUTTON_VIEW)]
  public class CloseButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private IWindowManager _windowManager;

    [Inject]
    public void Construct(IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _button.onClick.AddListener(_windowManager.Close);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(_windowManager.Close);
    }

    private void Reset()
    {
      _button = GetComponentInChildren<Button>();
    }
  }
}
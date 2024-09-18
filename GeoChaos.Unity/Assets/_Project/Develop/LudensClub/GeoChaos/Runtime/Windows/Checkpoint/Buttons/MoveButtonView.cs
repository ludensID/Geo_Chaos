using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Checkpoint
{
  [AddComponentMenu(ACC.Names.MOVE_BUTTON_VIEW)]
  public class MoveButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private IWindowManager _windowManager;

    [Inject]
    public void Construct(IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
      _windowManager.Open(WindowType.Map);
    }

    private void Reset()
    {
      _button = GetComponentInChildren<Button>();
    }
  }
}
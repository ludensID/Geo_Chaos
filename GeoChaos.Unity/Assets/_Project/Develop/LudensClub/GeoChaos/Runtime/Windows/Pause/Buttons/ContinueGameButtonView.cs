using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Pause
{
  [AddComponentMenu(ACC.Names.CONTINUE_GAME_BUTTON_VIEW)]
  public class ContinueGameButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;
    
    private IWindowManager _windowManager;

    [Inject]
    public void Construct(IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
      _windowManager.Close();
    }

    private void OnDestroy()
    {
      _button.onClick.RemoveListener(OnClick);
    }

    private void Reset()
    {
      _button = GetComponent<Button>();
    }
  }
}
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Menu
{
  [AddComponentMenu(ACC.Names.EXIT_GAME_BUTTON_VIEW)]
  public class ExitGameButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    [Inject]
    public void Construct()
    {
      _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif
      Application.Quit();
    }

    private void Reset()
    {
      _button = GetComponent<Button>();
    }
  }
}
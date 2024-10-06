using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Pause
{
  [AddComponentMenu(ACC.Names.EXIT_TO_MENU_BUTTON_VIEW)]
  public class ExitToMenuButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private GameStateMachine _gameStateMachine;
    private IWindowManager _windowManager;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine, IWindowManager windowManager)
    {
      _windowManager = windowManager;
      _gameStateMachine = gameStateMachine;
      _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
      _windowManager.Close();
      _gameStateMachine.SwitchState<MenuGameState, OnlyLoadScenePayload>(new OnlyLoadScenePayload()).Forget();
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
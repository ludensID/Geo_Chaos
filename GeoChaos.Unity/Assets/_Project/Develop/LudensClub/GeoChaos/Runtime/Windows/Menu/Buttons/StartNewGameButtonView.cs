using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Windows.Menu
{
  public class StartNewGameButtonView : MonoBehaviour
  {
    [SerializeField]
    private Button _button;

    private GameStateMachine _gameStateMachine;

    [Inject]
    public void Construct(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
      _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
      _gameStateMachine.SwitchState<GameplayGameState, StartNewGamePayload>(new StartNewGamePayload()).Forget();
    }

    private void Reset()
    {
      _button = GetComponent<Button>();
    }
  }
}
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using Zenject;

namespace LudensClub.GeoChaos.Debugging.Boot
{
  public class EditorInitializer : IInitializable
  {
    private readonly GameStateMachine _gameStateMachine;

    public EditorInitializer(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
      _gameStateMachine.SwitchState<GameplayGameState>().Forget();
    }
  }
}
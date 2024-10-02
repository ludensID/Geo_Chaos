using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class BootInitializer : IInitializable
  {
    private readonly GameStateMachine _gameStateMachine;

    public BootInitializer(GameStateMachine gameStateMachine)
    {
      _gameStateMachine = gameStateMachine;
    }

    public void Initialize()
    {
      _gameStateMachine.SwitchState<MenuGameState>().Forget();
    }
  }
}
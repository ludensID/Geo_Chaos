using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.GameStateMachine.States;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class ProjectInitializer : IInitializable
  {
    private readonly GameStateMachine.GameStateMachine _gameStateMachine;
    private readonly IStateFactory _stateFactory;

    public ProjectInitializer(GameStateMachine.GameStateMachine gameStateMachine, IStateFactory stateFactory)
    {
      _gameStateMachine = gameStateMachine;
      _stateFactory = stateFactory;
    }
      
    public void Initialize()
    {
      _gameStateMachine.RegisterState(_stateFactory.Create<LoadingState>());
      _gameStateMachine.RegisterState(_stateFactory.Create<GameplayState>());

      _gameStateMachine.SwitchState<LoadingState>().Forget();
    }
  }
}
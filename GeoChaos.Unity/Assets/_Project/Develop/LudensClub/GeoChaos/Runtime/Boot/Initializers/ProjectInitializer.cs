using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class ProjectInitializer : IInitializable
  {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IStateFactory _stateFactory;

    public ProjectInitializer(GameStateMachine gameStateMachine, IStateFactory stateFactory)
    {
      _gameStateMachine = gameStateMachine;
      _stateFactory = stateFactory;
    }
      
    public void Initialize()
    {
      _gameStateMachine.RegisterState(_stateFactory.Create<GameplayGameState>());
    }
  }
}
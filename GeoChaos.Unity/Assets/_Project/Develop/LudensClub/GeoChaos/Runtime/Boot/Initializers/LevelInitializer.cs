using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Boot
{
  public class LevelInitializer : IInitializable
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly IStateFactory _stateFactory;

    public LevelInitializer(LevelStateMachine levelStateMachine, IStateFactory stateFactory)
    {
      _levelStateMachine = levelStateMachine;
      _stateFactory = stateFactory;
    }
    
    public void Initialize()
    {
      _levelStateMachine.RegisterState(_stateFactory.Create<GameplayLevelState>());
      _levelStateMachine.RegisterState(_stateFactory.Create<PauseLevelState>());
      _levelStateMachine.RegisterState(_stateFactory.Create<RestartLevelState>());

      _levelStateMachine.SwitchState<GameplayLevelState>().Forget();
    }
  }
}
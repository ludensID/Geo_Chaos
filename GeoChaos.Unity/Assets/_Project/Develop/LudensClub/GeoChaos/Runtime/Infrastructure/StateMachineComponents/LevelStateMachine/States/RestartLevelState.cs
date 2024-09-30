using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class RestartLevelState : IState
  {
    private readonly LevelStateMachine _levelStateMachine;
    private readonly IRestartProcessor _restartProcessor;

    public RestartLevelState(LevelStateMachine levelStateMachine, IRestartProcessor restartProcessor)
    {
      _levelStateMachine = levelStateMachine;
      _restartProcessor = restartProcessor;
    }
    
    public async UniTask Enter()
    {
      await _restartProcessor.RestartAsync();
      await _levelStateMachine.SwitchState<GameplayLevelState>();
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}
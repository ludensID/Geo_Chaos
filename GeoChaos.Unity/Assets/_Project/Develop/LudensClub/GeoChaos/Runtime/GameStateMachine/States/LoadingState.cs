using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;
using LudensClub.GeoChaos.Runtime.Persistence;

namespace LudensClub.GeoChaos.Runtime.GameStateMachine.States
{
  public class LoadingState : IState
  {
    private readonly IPersistenceService _persistence;
    private readonly GameStateMachine _gameStateMachine;

    public LoadingState(IPersistenceService persistence, GameStateMachine gameStateMachine)
    {
      _persistence = persistence;
      _gameStateMachine = gameStateMachine;
    }

    public async UniTask Enter()
    {
      await _persistence.LoadAsync();
      await _gameStateMachine.SwitchState<GameplayState>();
    }

    public UniTask Exit()
    {
      return default(UniTask);
    }
  }
}
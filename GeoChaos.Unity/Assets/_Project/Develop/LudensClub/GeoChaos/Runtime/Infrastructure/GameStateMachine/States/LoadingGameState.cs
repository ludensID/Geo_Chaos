using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;
using LudensClub.GeoChaos.Runtime.Persistence;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class LoadingGameState : IState
  {
    private readonly IPersistenceService _persistence;
    private readonly GameStateMachine _gameStateMachine;

    public LoadingGameState(IPersistenceService persistence, GameStateMachine gameStateMachine)
    {
      _persistence = persistence;
      _gameStateMachine = gameStateMachine;
    }

    public async UniTask Enter()
    {
      await _persistence.LoadAsync();
      await _gameStateMachine.SwitchState<GameplayGameState>();
    }

    public UniTask Exit()
    {
      return default(UniTask);
    }
  }
}
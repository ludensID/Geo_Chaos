using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class PauseLevelState : IState
  {
    private readonly IGameplayPause _pause;

    public PauseLevelState(IGameplayPause pause)
    {
      _pause = pause;
    }

    public UniTask Enter()
    {
      _pause.SetPause();
      return UniTask.CompletedTask;
    }

    public UniTask Exit()
    {
      _pause.UnsetPause();
      return UniTask.CompletedTask;
    }
  }
}
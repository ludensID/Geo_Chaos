using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class GameplayLevelState : IState
  {
    public UniTask Enter()
    {
      return UniTask.CompletedTask;
    }

    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }
  }
}
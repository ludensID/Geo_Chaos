using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class RestartLevelState : IState
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
using Cysharp.Threading.Tasks;
using LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine;

namespace LudensClub.GeoChaos.Runtime.GameStateMachine.States
{
  public class GameplayState : IState 
  {
    public UniTask Exit()
    {
      return UniTask.CompletedTask;
    }

    public UniTask Enter()
    {
      return UniTask.CompletedTask;
    }
  }
}
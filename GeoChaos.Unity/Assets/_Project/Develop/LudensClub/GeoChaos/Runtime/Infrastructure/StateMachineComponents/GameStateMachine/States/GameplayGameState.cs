using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class GameplayGameState : IState 
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
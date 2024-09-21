using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine
{
  public interface IState : IExitableState
  {
    UniTask Enter();
  }
}
using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public interface IState : IExitableState
  {
    UniTask Enter();
  }
}
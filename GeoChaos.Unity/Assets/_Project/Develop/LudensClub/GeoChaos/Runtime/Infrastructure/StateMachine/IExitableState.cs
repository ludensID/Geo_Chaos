using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine
{
  public interface IExitableState
  {
    UniTask Exit();
  }
}
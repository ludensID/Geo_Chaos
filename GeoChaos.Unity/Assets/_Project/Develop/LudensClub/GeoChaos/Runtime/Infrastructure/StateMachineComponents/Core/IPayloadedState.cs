using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public interface IPaylodedState<TPayload> : IExitableState
  {
    public UniTask Enter(TPayload payload);
  }
}
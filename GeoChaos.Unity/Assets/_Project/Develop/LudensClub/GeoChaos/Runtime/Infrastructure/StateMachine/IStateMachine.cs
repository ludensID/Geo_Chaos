using Cysharp.Threading.Tasks;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine
{
  public interface IStateMachine
  {
    UniTask SwitchState<TState>() where TState : class, IState;
    UniTask SwitchState<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
    void RegisterState<TState>(TState state) where TState : IExitableState;
  }
}
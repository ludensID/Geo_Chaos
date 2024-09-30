namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public interface IStateFactory
  {
    TState Create<TState>() where TState : IExitableState;
  }
}
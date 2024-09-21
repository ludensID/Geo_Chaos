namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachine
{
  public interface IStateFactory
  {
    TState Create<TState>() where TState : IExitableState;
  }
}
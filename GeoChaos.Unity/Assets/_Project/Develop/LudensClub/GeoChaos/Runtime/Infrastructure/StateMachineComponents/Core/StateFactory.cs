using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.StateMachineComponents
{
  public class StateFactory : IStateFactory
  {
    private readonly IInstantiator _instantiator;

    public StateFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public TState Create<TState>() where TState : IExitableState
    {
      return _instantiator.Instantiate<TState>();
    }
  }
}
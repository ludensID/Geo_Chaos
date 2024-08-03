using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public class NodeStrategyFactory : INodeStrategyFactory
  {
    private readonly IInstantiator _instantiator;

    public NodeStrategyFactory(IInstantiator instantiator)
    {
      _instantiator = instantiator;
    }

    public TStrategy Create<TStrategy>() where TStrategy : INodeStrategy
    {
      return _instantiator.Instantiate<TStrategy>();
    }
  }
}
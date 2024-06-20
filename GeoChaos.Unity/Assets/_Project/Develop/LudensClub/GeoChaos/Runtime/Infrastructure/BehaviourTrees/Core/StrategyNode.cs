namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public abstract class StrategyNode : Node
  {
    public INodeStrategy Strategy;

    protected StrategyNode(INodeStrategy strategy)
    {
      Strategy = strategy;
      strategy.Entity = _entity;
    }
  }
}
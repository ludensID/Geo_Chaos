namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public interface INodeStrategyFactory
  {
    TStrategy Create<TStrategy>() where TStrategy : INodeStrategy;
  }
}
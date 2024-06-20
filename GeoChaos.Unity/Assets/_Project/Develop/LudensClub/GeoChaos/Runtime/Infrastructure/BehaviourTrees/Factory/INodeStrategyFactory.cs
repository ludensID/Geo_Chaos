namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public interface INodeStrategyFactory
  {
    TStrategy Create<TStrategy>() where TStrategy : INodeStrategy;
  }
}
namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public interface IConditionStrategy : INodeStrategy
  {
    public bool Check();
  }
}
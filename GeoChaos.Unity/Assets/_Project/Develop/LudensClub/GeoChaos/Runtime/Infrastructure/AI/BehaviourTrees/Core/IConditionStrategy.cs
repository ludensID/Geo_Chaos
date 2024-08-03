namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public interface IConditionStrategy : INodeStrategy
  {
    public bool Check();
  }
}
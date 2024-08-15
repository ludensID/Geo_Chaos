namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public interface IActionStrategy : INodeStrategy
  {
    public BehaviourStatus Execute();
  }
}
namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public interface IActionStrategy : INodeStrategy
  {
    public BehaviourStatus Execute();
  }
}
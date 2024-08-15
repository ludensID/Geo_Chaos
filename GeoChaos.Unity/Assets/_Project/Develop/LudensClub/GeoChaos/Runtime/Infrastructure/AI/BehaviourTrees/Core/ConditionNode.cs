namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public class ConditionNode : StrategyNode
  {
    private readonly IConditionStrategy _strategy;

    public ConditionNode(IConditionStrategy strategy, EcsEntity entity) : base(strategy, entity)
    {
      _strategy = strategy;
    }

    public override BehaviourStatus Run()
    {
      Status = _strategy.Check() ? TRUE : FALSE;
      return Status;
    }
  }
}
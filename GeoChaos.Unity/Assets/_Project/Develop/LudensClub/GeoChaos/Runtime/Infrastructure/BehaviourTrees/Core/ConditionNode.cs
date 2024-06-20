namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class ConditionNode : StrategyNode
  {
    private readonly IConditionStrategy _strategy;

    public ConditionNode(IConditionStrategy strategy) : base(strategy)
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
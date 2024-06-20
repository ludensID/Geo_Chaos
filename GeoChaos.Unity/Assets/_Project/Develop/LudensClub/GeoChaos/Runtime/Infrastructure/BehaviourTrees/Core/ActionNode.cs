namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class ActionNode : StrategyNode
  {
    private readonly IActionStrategy _strategy;
    
    public ActionNode(IActionStrategy strategy) : base(strategy)
    {
      _strategy = strategy;
    }

    public override BehaviourStatus Run()
    {
      Status = _strategy.Execute();
      return Status;
    }
  }
}
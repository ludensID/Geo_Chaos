using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class ActionNode : StrategyNode
  {
    private readonly IActionStrategy _strategy;
    
    public ActionNode(IActionStrategy strategy, EcsPackedEntity entity) : base(strategy, entity)
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
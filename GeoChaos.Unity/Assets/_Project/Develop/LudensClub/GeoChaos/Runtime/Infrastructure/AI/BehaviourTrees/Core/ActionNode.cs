﻿namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public class ActionNode : StrategyNode
  {
    private readonly IActionStrategy _strategy;
    
    public ActionNode(IActionStrategy strategy, EcsEntity entity) : base(strategy, entity)
    {
      _strategy = strategy;
    }

    public override BehaviourStatus Run()
    {
      Status = _strategy.Execute();
      return Status;
    }

    public override void Reset()
    {
      (_strategy as IResetStrategy)?.Reset();
      base.Reset();
    }
  }
}
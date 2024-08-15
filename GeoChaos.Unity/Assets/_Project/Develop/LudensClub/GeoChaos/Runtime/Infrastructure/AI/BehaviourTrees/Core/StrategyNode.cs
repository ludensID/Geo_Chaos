using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public abstract class StrategyNode : Node
  {
    [SerializeReference]
    public INodeStrategy Strategy;

    protected StrategyNode(INodeStrategy strategy, EcsEntity entity) : base(entity)
    {
      Strategy = strategy;
      strategy.Entity = _entity;
    }
  }
}
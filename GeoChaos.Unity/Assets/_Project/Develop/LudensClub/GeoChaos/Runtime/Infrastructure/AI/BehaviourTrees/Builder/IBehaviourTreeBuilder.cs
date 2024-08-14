using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public interface IBehaviourTreeBuilder
  {
    IBehaviourTreeBuilder Create(EcsPackedEntity entity);
    IBehaviourTreeBuilder Add(Node node);
    IBehaviourTreeBuilder ToChild();
    IBehaviourTreeBuilder ToParent();
    BehaviourTree End();
    IBehaviourTreeBuilder AddSelector();
    IBehaviourTreeBuilder AddSequence();
    IBehaviourTreeBuilder AddAction<TStrategy>() where TStrategy : IActionStrategy;
    IBehaviourTreeBuilder AddCondition<TStrategy>() where TStrategy : IConditionStrategy;
  }
}
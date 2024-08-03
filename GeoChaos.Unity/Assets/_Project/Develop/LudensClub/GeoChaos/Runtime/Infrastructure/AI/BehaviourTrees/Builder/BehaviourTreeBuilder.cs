using System.Collections.Generic;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public class BehaviourTreeBuilder : IBehaviourTreeBuilder
  {
    private readonly INodeStrategyFactory _factory;
    private readonly Stack<Node> _stack = new Stack<Node>();
    
    private BehaviourTree _bt;
    private Node _lastChild;

    public BehaviourTreeBuilder(INodeStrategyFactory factory)
    {
      _factory = factory;
    }

    public IBehaviourTreeBuilder Create(EcsPackedEntity entity)
    {
      Reset();

      _bt = new BehaviourTree(entity);
      _stack.Push(_bt);
      return this;
    }

    public IBehaviourTreeBuilder Add(Node node)
    {
      _stack.Peek().AddChild(node);
      _lastChild = node;
      return this;
    }

    public IBehaviourTreeBuilder AddAction(IActionStrategy strategy)
    {
      return Add(new ActionNode(strategy, _bt.Entity));
    }

    public IBehaviourTreeBuilder AddAction<TStrategy>() where TStrategy : IActionStrategy
    {
      return Add(new ActionNode(_factory.Create<TStrategy>(), _bt.Entity));
    }

    public IBehaviourTreeBuilder AddCondition(IConditionStrategy strategy)
    {
      return Add(new ConditionNode(strategy, _bt.Entity));
    }
    
    public IBehaviourTreeBuilder AddCondition<TStrategy>() where TStrategy : IConditionStrategy
    {
      return Add(new ConditionNode(_factory.Create<TStrategy>(), _bt.Entity));
    }
    
    public IBehaviourTreeBuilder AddSelector()
    {
      return Add(new SelectorNode());
    }

    public IBehaviourTreeBuilder AddSequence()
    {
      return Add(new SequenceNode());
    }

    public IBehaviourTreeBuilder ToChild()
    {
      _stack.Push(_lastChild);
      _lastChild = null;
      return this;
    }

    public IBehaviourTreeBuilder ToParent()
    {
      _stack.Pop();
      _lastChild = null;
      return this;
    }

    public BehaviourTree End()
    {
      return _bt;
    }

    private void Reset()
    {
      _stack.Clear();
      _lastChild = null;
    }
  }
}
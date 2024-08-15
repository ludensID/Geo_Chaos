using System;
using System.Collections.Generic;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  [Serializable]
  public abstract class Node
  {
    public const BehaviourStatus FALSE = BehaviourStatus.Failure;
    public const BehaviourStatus TRUE = BehaviourStatus.Success;
    public const BehaviourStatus CONTINUE = BehaviourStatus.Running;
    
    public List<Node> Children = new List<Node>();
    
    protected EcsEntity _entity = new EcsEntity();
    
    public EcsEntity Entity => _entity;
    
    [ShowInInspector]
    public BehaviourStatus Status { get; set; }

    public Node()
    {
    }

    public Node(EcsEntity entity)
    {
      _entity = entity;
    }

    public void AddChild(Node child)
    {
      Children.Add(child);
      child._entity = Entity;
    }

    public abstract BehaviourStatus Run();

    public virtual void Reset()
    {
      Status = default(BehaviourStatus);
    }
  }
}
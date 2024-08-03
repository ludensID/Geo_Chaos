using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
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
    
    protected EcsPackedEntity _entity;
    
    public EcsPackedEntity Entity => _entity;
    
    [ShowInInspector]
    public BehaviourStatus Status { get; set; }

    public Node()
    {
    }

    public Node(EcsPackedEntity entity)
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
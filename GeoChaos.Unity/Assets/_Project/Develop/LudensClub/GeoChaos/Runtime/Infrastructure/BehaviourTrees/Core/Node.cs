using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  [Serializable]
  public abstract class Node
  {
    public const BehaviourStatus FALSE = BehaviourStatus.Failure;
    public const BehaviourStatus TRUE = BehaviourStatus.Success;
    
    public List<Node> Children = new List<Node>();
    
    protected EcsPackedEntity _entity;
    
    public EcsPackedEntity Entity => _entity;
    public BehaviourStatus Status { get; set; }

    public void AddChild(Node child)
    {
      Children.Add(child);
      child._entity = Entity;
    }

    public abstract BehaviourStatus Run();
  }
}
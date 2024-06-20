using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  [Serializable]
  public class BehaviourTree : Node
  {
    public BehaviourTree(EcsPackedEntity entity)
    {
      _entity = entity;
    }
    
    public override BehaviourStatus Run()
    {
      foreach (Node child in Children)
      {
        Status = child.Run();
        if (Status != TRUE)
          return Status;
      }

      Status = TRUE;
      return Status;
    }
  }
}
using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  [Serializable]
  public class BehaviourTree : Node
  {
    public BehaviourTree(EcsPackedEntity entity) : base(entity)
    {
    }
    
    public override BehaviourStatus Run()
    {
      for (int i = 0; i < Children.Count; i++)
      {
        Status = Children[i].Run();
        if (Status != FALSE)
          return Status;
      }

      Status = FALSE;
      return Status;
    }
  }
}
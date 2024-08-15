using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  [Serializable]
  public class BehaviourTree : SelectorNode
  {
    public BehaviourTree(EcsEntity entity)
    {
      Entity.Copy(entity);
    }
  }
}
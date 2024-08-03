using System;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  [Serializable]
  public class BehaviourTree : SelectorNode
  {
    public BehaviourTree(EcsPackedEntity entity) : base(entity)
    {
    }
  }
}
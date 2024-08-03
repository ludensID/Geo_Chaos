using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public abstract class CompositeNode : Node
  {
    protected Node RunningNode;

    public CompositeNode()
    {
    }

    public CompositeNode(EcsPackedEntity entity) : base(entity)
    {
    }

    public override void Reset()
    {
      if (RunningNode != null)
        ResetRunningNode();
      
      base.Reset();
    }

    protected void ResetRunningNode()
    {
      RunningNode.Reset();
      RunningNode = null;
    }
  }
}
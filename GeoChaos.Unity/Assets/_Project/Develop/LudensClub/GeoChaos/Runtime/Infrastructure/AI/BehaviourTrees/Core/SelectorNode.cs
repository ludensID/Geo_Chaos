namespace LudensClub.GeoChaos.Runtime.Infrastructure.AI.BehaviourTrees
{
  public class SelectorNode : CompositeNode
  {
    public SelectorNode()
    {
    }

    public SelectorNode(EcsEntity entity) : base(entity)
    {
    }

    public override BehaviourStatus Run()
    {
      for (int i = 0; i < Children.Count; i++)
      {
        Node child = Children[i];
        Status = child.Run();
        if (Status != FALSE)
        {
          if (RunningNode != null && RunningNode != child)
            ResetRunningNode();

          RunningNode = Status == CONTINUE ? child : null;

          return Status;
        }

        if (RunningNode == child)
          RunningNode = null;
      }

      Status = FALSE;
      return Status;
    }
  }
}
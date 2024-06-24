namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class SequenceNode : CompositeNode
  {
    public override BehaviourStatus Run()
    {
      for (int i = 0; i < Children.Count; i++)
      {
        Node child = Children[i];
        Status = child.Run();
        if (Status != TRUE)
        {
          if(RunningNode != null && RunningNode != child)
            ResetRunningNode();

          RunningNode = Status == CONTINUE ? child : null;
          
          return Status;
        }

        if (RunningNode == child)
          RunningNode = null;
      }

      Status = TRUE;
      return Status;
    }
  }
}
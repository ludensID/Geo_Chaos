namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class SequenceNode : Node
  {
    public override BehaviourStatus Run()
    {
      for (int i = 0; i < Children.Count; i++)
      {
        Status = Children[i].Run();
        if (Status != TRUE)
          return Status;
      }

      Status = TRUE;
      return Status;
    }
  }
}
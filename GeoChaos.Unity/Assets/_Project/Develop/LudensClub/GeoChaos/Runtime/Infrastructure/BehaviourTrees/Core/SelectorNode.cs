﻿namespace LudensClub.GeoChaos.Runtime.Infrastructure.BehaviourTrees
{
  public class SelectorNode : Node
  {
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
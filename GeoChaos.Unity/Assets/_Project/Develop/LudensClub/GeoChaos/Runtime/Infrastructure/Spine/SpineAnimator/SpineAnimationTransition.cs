using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimationTransition
  {
    private readonly Predicate<SpineCondition> _executor = x => x.Execute();
    
    public SpineTransition Data;

    public SpineAnimationState Destination;

    public bool Execute()
    {
      return Data.Conditions.Count == 0 || Data.Conditions.AllNonAlloc(_executor);
    }
  }
}
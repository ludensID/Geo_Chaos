using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimationTransition<TAnimationEnum> where TAnimationEnum : Enum
  {
    private readonly Predicate<ISpineCondition> _executor = x => x.Execute();
    
    public ISpineTransition<TAnimationEnum> Data;

    public SpineAnimationState<TAnimationEnum> Destination;

    public bool Execute()
    {
      return Data.Conditions.Count == 0 || Data.Conditions.AllNonAlloc(_executor);
    }
  }
}
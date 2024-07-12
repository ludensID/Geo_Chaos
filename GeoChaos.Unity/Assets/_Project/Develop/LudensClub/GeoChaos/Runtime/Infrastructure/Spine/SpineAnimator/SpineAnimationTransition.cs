using System;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimationTransition<TAnimationEnum> where TAnimationEnum : Enum
  {
    private readonly ConditionExecutor _executor = new ConditionExecutor();
    
    public ISpineTransition<TAnimationEnum> Data;

    public SpineAnimationState<TAnimationEnum> Destination;

    public bool Execute()
    {
      return Data.Conditions.Count == 0 || Data.Conditions.AllNonAlloc(_executor);
    }
    
    public class ConditionExecutor : IPredicate<ISpineCondition>
    {
      public bool Predicate(ISpineCondition obj)
      {
        return obj.Execute();
      }
    }
  }
}
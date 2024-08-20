using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimationState
  {
    public readonly List<SpineAnimationTransition> Transitions =
      new List<SpineAnimationTransition>();
    
    public ConfigurableSpineAnimation Animation;

    public SpineAnimationState(ConfigurableSpineAnimation animation)
    {
      Animation = animation;
    }
    
    public SpineAnimationState() : this(null)
    {
    }

    public SpineAnimationTransition FindFirstCompletedCondition() =>
      Transitions.Find(x => x.Execute());
  }
}
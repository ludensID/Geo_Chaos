using System;
using System.Collections.Generic;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineAnimationState<TAnimationEnum> where TAnimationEnum : Enum
  {
    [HideInInspector]
    public readonly List<SpineAnimationTransition<TAnimationEnum>> Transitions =
      new List<SpineAnimationTransition<TAnimationEnum>>();
    
    public ConfigurableSpineAnimation<TAnimationEnum> Animation;

    public SpineAnimationState(ConfigurableSpineAnimation<TAnimationEnum> animation)
    {
      Animation = animation;
    }
    
    public SpineAnimationState() : this(null)
    {
    }

    public SpineAnimationTransition<TAnimationEnum> FindFirstCompletedCondition() =>
      Transitions.Find(x => x.Execute());
  }
}
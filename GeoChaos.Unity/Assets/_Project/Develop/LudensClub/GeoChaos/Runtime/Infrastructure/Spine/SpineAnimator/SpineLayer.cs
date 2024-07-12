using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineLayer<TAnimationEnum> where TAnimationEnum : Enum
  {
    public TAnimationEnum StartAnimation;
    public List<ConfigurableSpineAnimation<TAnimationEnum>> Animations;
  }
}
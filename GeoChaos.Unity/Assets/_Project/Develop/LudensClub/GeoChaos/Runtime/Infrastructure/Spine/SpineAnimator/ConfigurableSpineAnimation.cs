using System;
using Spine.Unity;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class ConfigurableSpineAnimation<TAnimationEnum> where TAnimationEnum : Enum
  {
    public TAnimationEnum Name;

    public AnimationReferenceAsset Asset;

    public float Speed = 1;

    public bool IsLoop;
  }
}
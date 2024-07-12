using System;
using Spine.Unity;
using TriInspector;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  [DeclareHorizontalGroup(nameof(ConfigurableSpineAnimation<TAnimationEnum>))]
  public class ConfigurableSpineAnimation<TAnimationEnum> where TAnimationEnum : Enum
  {
    [GroupNext(nameof(ConfigurableSpineAnimation<TAnimationEnum>))]
    [HideLabel]
    public TAnimationEnum Name;
    [HideLabel]
    public AnimationReferenceAsset Asset;
    [LabelWidth(60)]
    public bool IsLoop;
  }
}
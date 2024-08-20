using System;
using System.Collections.Generic;
using Spine.Unity;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class SpineLayer
  {
    [SpineAnimation]
    public string StartAnimation;

    public List<ConfigurableSpineAnimation> Animations;
  }
}
using System;
using Spine.Unity;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Spine
{
  [Serializable]
  public class ConfigurableSpineAnimation
  {
    [SpineAnimation]
    public string Name;
      
    public float Speed = 1;

    public bool IsLoop;
  }
}
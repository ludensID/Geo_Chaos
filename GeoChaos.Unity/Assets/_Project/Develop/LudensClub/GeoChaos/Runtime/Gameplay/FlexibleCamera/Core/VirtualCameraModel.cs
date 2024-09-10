using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraModel
  {
    public readonly CallbackProperty<float> VerticalDamping = new CallbackProperty<float>();
    public readonly CallbackProperty<float> EdgeVerticalOffset = new CallbackProperty<float>();
  }
}
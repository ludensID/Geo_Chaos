using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraModel
  {
    public readonly CallbackValue<float> VerticalDamping = new CallbackValue<float>();
    public readonly CallbackValue<float> VerticalShift = new CallbackValue<float>();
    
    public readonly CallbackValue<float> EdgeVerticalShift = new CallbackValue<float>();
    public readonly CallbackValue<float> VerticalViewShift = new CallbackValue<float>();
  }
}
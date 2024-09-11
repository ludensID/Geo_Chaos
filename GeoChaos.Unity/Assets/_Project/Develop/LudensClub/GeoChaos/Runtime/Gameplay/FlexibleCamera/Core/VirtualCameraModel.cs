using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraModel
  {
    public readonly CallbackValue<float> VerticalDamping = new CallbackValue<float>();
    public readonly CallbackValue<float> VerticalOffset = new CallbackValue<float>();
    
    public readonly CallbackValue<float> EdgeVerticalOffset = new CallbackValue<float>();
    public readonly CallbackValue<float> VerticalViewOffset = new CallbackValue<float>();
  }
}
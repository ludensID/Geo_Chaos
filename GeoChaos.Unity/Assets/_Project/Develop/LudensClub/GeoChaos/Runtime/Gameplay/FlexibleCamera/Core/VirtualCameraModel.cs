using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  public class VirtualCameraModel
  {
    public readonly CallbackValue<float> VerticalDamping = new CallbackValue<float>();
    public readonly CallbackValue<Vector2> SmoothShift = new CallbackValue<Vector2>();
    public readonly CallbackValue<Vector2> TargetShift = new CallbackValue<Vector2>();
    
    public readonly CallbackValue<Vector2> AimShift = new CallbackValue<Vector2>();
    public readonly CallbackValue<float> EdgeVerticalShift = new CallbackValue<float>();
    public readonly CallbackValue<float> VerticalViewShift = new CallbackValue<float>();
  }
}
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [CreateAssetMenu(menuName = CAC.Names.CAMERA_MENU, fileName = CAC.Names.CAMERA_FILE)]
  public class CameraConfig : ScriptableObject
  {
    [Title("Rotation")]
    public float RotationTime;

    [Title("Vertical Damping")]
    public float VerticalDampingInterpolationTime;

    public float DefaultVerticalDamping;
    public float FallVerticalDamping;

    [Title("Shift")]
    public float ShiftInterpolationTime;

    [Header("Edge")]
    public float EdgeVerticalShift;

    [Header("Look")]
    public float DelayBeforeLook;

    public float LookUpShift;
    public float LookDownShift;

    [Header("Aim")]
    public float DelayBeforeAim;

    public float AimShiftDistance;
  }
}
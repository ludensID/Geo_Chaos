using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.FlexibleCamera
{
  [CreateAssetMenu(menuName = CAC.Names.CAMERA_MENU, fileName = CAC.Names.CAMERA_FILE)]
  public class CameraConfig : ScriptableObject
  {
    public float RotationTime;
  }
}
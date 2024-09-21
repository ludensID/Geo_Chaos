using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.FADING_PLATFORM_MENU, fileName = CAC.Names.FADING_PLATFORM_FILE)]
  public class FadingPlatformConfig : ScriptableObject
  {
    public float FadeTime;
    public float AppearCooldown;
  }
}
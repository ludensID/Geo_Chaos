using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.CURTAIN_CONFIG_MENU, fileName = CAC.Names.CURTAIN_CONFIG_FILE)]
  public class CurtainConfig : ScriptableObject
  {
    public float TimeToOpen;
    public float TimeToClose;
  }
}
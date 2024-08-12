using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.TONGUE_CONFIG_MENU, fileName = CAC.Names.TONGUE_CONFIG_FILE)]
  public class TongueConfig : ScriptableObject
  {
    public float Speed;
  }
}
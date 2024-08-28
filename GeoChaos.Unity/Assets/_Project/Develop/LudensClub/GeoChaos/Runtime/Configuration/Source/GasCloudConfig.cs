using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.GAS_CLOUD_CONFIG_MENU, fileName = CAC.Names.GAS_CLOUD_CONFIG_FILE)]
  public class GasCloudConfig : ScriptableObject
  {
    public float LifeTime;
    public float MinSize;
    public float ExpandSpeed;
  }
}
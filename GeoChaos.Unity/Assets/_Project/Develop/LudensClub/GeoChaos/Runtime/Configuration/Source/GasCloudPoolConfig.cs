using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.GAS_CLOUD_POOL_MENU, fileName = CAC.Names.GAS_CLOUD_POOL_FILE)]
  public class GasCloudPoolConfig : ScriptableObject
  {
    [HideLabel]
    public PoolConfig Pool;
  }
}
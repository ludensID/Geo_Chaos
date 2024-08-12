using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.TONGUE_POOL_CONFIG_MENU, fileName = CAC.Names.TONGUE_POOL_CONFIG_FILE)]
  public class TonguePoolConfig : ScriptableObject
  {
    [HideLabel]
    public PoolConfig Pool;
  }
}
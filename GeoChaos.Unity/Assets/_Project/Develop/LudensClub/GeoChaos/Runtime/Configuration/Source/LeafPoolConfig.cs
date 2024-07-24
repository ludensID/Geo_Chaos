using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(menuName = CAC.Names.LEAF_POOL_MENU, fileName = CAC.Names.LEAF_POOL_FILE)]
  public class LeafPoolConfig : ScriptableObject
  {
    [HideLabel]
    public PoolConfig Pool;
  }
}
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.Names.SHARD_POOL_FILE, menuName = CAC.Names.SHARD_POOL_MENU, order = 0)]
  public class ShardPoolConfig : ScriptableObject
  {
    [HideLabel]
    public PoolConfig Pool;
  }
}
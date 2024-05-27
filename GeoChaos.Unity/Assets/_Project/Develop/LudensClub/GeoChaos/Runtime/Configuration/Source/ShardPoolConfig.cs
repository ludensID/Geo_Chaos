using LudensClub.GeoChaos.Runtime.Constants;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Configuration
{
  [CreateAssetMenu(fileName = CAC.SHARD_FILE, menuName = CAC.SHARD_MENU, order = 0)]
  public class ShardPoolConfig : ScriptableObject
  {
    public int InstanceCount;
    public float DistanceFromOrigin;
  }
}
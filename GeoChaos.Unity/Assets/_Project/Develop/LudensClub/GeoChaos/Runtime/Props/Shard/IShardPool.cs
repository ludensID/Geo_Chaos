using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
{
  public interface IShardPool : IPushable
  {
    ShardView Pull();
    ShardView Pull(Vector3 position, Quaternion rotation, Transform parent = null);
  }
}
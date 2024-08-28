using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Shard
{
  public interface IShardFactory
  {
    EcsEntity Create(Vector3 position);
  }
}
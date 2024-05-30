using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Shard
{
  public interface IShardFactory
  {
    EcsEntity Create();
  }
}
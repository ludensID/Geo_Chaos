using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Shard
{
  public class ShardPool : ViewPool<ShardView>
  {
    public ShardPool(IConfigProvider configProvider, IViewFactory factory) 
      : base(configProvider.Get<ShardPoolConfig>().Pool, factory)
    {
    }

    public override EntityType Id => EntityType.Shard;
  }
}
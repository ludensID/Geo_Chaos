using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Props.Shard
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
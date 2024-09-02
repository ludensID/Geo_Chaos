using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class GasCloudPool : ViewPool<GasCloudView>
  {
    public override EntityType Id => EntityType.GasCloud;

    public GasCloudPool(IConfigProvider configProvider, IViewFactory factory) 
      : base(configProvider.Get<GasCloudPoolConfig>().Pool, factory)
    {
    }
  }
}
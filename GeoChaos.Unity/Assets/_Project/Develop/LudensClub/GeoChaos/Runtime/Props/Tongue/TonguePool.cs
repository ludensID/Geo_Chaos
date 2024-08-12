using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Props.Tongue
{
  public class TonguePool : ViewPool<TongueView>
  {
    public override EntityType Id => EntityType.Tongue; 

    public TonguePool(IConfigProvider configProvider, IViewFactory factory) 
        : base(configProvider.Get<TonguePoolConfig>().Pool, factory)
    {
    }
  }
}
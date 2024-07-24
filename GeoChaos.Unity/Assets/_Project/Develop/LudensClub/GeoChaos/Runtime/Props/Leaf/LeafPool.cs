using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Props.Leaf
{
  public class LeafPool : ViewPool<LeafView>
  {
    public LeafPool(IConfigProvider configProvider, IViewFactory factory) 
      : base(configProvider.Get<LeafPoolConfig>().Pool, factory)
    {
    }

    public override EntityType Id => EntityType.Leaf;
  }
}
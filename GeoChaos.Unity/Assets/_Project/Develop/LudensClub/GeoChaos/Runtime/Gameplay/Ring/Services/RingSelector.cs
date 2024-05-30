using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  public class RingSelector : EcsEntitySelector
  {
    public RingSelector(ISelectionAlgorithmFactory factory, IConfigProvider configProvider)
    {
      var config = configProvider.Get<HeroConfig>();
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<InRadiusSelectionAlgorithm>(config.HookRadius),
        factory.Create<ReachedRingSelectionAlgorithm>(),
        factory.Create<InTargetViewSelectionAlgorithm>(config.RingViewAngle),
        factory.Create<NearestTargetSelectionAlgorithm>()
      });
    }
  }
}
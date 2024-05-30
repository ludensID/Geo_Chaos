using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class EnemySelector : EcsEntitySelector
  {
    public EnemySelector(ISelectionAlgorithmFactory factory, IConfigProvider configProvider)
    {
      var config = configProvider.Get<HeroConfig>();
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<InRadiusSelectionAlgorithm>(config.ShootRadius),
        factory.Create<ReachedEnemySelectionAlgorithm>(),
        factory.Create<InTargetViewSelectionAlgorithm>(config.EnemyViewAngle),
        factory.Create<NearestTargetSelectionAlgorithm>()
      });
    }
  }
}
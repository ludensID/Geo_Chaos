using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class AimedLamaSelector : EcsEntitySelector
  {
    public AimedLamaSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<LamasTargetInRadiusAlgorithm>(),
        factory.Create<LamasTargetInViewAlgorithm>(),
        factory.Create<ReachedEnemySelectionAlgorithm>(),
        factory.Create<LamasTargetInBoundsAlgorithm>()
      });
    }    
  }
}
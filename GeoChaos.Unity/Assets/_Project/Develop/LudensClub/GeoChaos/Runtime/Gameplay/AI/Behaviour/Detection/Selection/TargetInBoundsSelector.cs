using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection
{
  public class TargetInBoundsSelector : EcsEntitySelector
  {
    public TargetInBoundsSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<TargetInHorizontalBoundsAlgorithm>(),
        factory.Create<TargetInVerticalBoundsAlgorithm>()
      });
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class FrogTargetInViewSelector : EcsEntitySelector
  {
    public FrogTargetInViewSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<TargetInBoundsAlgorithm>(),
        factory.Create<TargetNearByVerticalAlgorithm>(),
        factory.Create<TargetInViewAlgorithm>()
      });
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class FrogTargetInFrontSelector : EcsEntitySelector
  {
    public FrogTargetInFrontSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<TargetNearByVerticalAlgorithm>(),
        factory.Create<TargetInViewAlgorithm>(),
        factory.Create<TargetInRadiusAlgorithm>()
      });
    }
  }
}
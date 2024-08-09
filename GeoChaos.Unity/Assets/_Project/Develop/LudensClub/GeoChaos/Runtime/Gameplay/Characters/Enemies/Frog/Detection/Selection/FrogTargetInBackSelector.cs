using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Detection
{
  public class FrogTargetInBackSelector : EcsEntitySelector
  {
    public FrogTargetInBackSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<TargetNearByVerticalAlgorithm>(),
        factory.Create<TargetInBackViewAlgorithm>(),
        factory.Create<TargetInRadiusAlgorithm>()
      });
    }
  }
}
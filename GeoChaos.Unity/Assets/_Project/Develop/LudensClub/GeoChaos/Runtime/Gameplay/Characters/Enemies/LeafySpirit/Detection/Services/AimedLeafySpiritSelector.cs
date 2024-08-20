using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Detection
{
  public class AimedLeafySpiritSelector : EcsEntitySelector
  {
    public AimedLeafySpiritSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<TargetInHorizontalBoundsAlgorithm>(),
        factory.Create<TargetInVerticalBoundsAlgorithm>(),
        factory.Create<TargetReachedAlgorithm>()
      });
    }
  }
}
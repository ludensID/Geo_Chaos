﻿using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.Detection
{
  public class AimedLamaSelector : EcsEntitySelector
  {
    public AimedLamaSelector(ISelectionAlgorithmFactory factory)
    {
      _algorithms.AddRange(new ISelectionAlgorithm[]
      {
        factory.Create<LamaTargetInRadiusAlgorithm>(),
        factory.Create<LamaTargetInViewAlgorithm>(),
        factory.Create<ReachedEnemySelectionAlgorithm>(),
        factory.Create<LamaTargetInBoundsAlgorithm>()
      });
    }    
  }
}
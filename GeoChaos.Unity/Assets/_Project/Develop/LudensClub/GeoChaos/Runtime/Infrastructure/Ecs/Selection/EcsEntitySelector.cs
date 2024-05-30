using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Selection
{
  public class EcsEntitySelector : IEcsEntitySelector
  {
    protected List<ISelectionAlgorithm> _algorithms = new List<ISelectionAlgorithm>();
    
    public void Select(EcsEntities origins, EcsEntities targets, EcsEntities selections)
    {
      foreach (EcsEntity target in targets)
      {
        target.Has<Selected>(true);
      }
      
      foreach (ISelectionAlgorithm algorithm in _algorithms)
      {
        algorithm.Select(origins, selections);
      }
    }
  }
}
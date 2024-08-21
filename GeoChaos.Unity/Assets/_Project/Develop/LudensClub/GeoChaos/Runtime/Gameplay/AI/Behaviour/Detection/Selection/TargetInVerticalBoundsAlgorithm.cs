using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Detection
{
  public class TargetInVerticalBoundsAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        float originPoint = origin.Get<ViewRef>().View.transform.position.y;
        Rect bounds = selection.Get<PatrolBounds>().Bounds;
        
        if (originPoint < bounds.yMin || bounds.yMax < originPoint)
          selection.Del<Marked>();
      }
    }
  }
}
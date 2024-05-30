using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class NearestTargetSelectionAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities selections)
    {
      var minDistance = float.MaxValue;
      EcsEntity nearestRing = null;
      
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in selections)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        float distance = Vector3.Distance(selectionPosition,  originPosition);
        if (distance < minDistance)
        {
          minDistance = distance;
          nearestRing = selection;
        }

        selection.Del<Selected>();
      }

      nearestRing?.Add<Selected>();
    }
  }
}
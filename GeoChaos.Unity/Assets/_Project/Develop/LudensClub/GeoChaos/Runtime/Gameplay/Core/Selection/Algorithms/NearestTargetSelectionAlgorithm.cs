using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class NearestTargetSelectionAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      var minDistance = float.MaxValue;
      var nearestTarget = new EcsEntity(origins.World, -1);

      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        float distance = Vector3.Distance(selectionPosition, originPosition);
        if (distance < minDistance)
        {
          minDistance = distance;
          nearestTarget.Entity = selection.Entity;
        }

        selection.Del<Marked>();
      }

      if (nearestTarget.IsAlive())
        nearestTarget.Add<Marked>();
    }
  }
}
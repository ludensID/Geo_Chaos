using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class NearestTargetSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly EcsEntity _nearestTarget = new EcsEntity();

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      var minDistance = float.MaxValue;
      _nearestTarget.SetWorld(origins.World);

      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;
        float distance = Vector3.Distance(selectionPosition, originPosition);
        if (distance < minDistance)
        {
          minDistance = distance;
          _nearestTarget.Entity = selection.Entity;
        }

        selection.Del<Marked>();
      }

      if (_nearestTarget.IsAlive())
        _nearestTarget.Add<Marked>();
    }
  }
}
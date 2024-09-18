using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  public class NearestRingSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly EcsEntity _nearestTarget = new EcsEntity();

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      var minDistance = float.MaxValue;
      _nearestTarget.SetWorld(origins.World);

      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector2 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector2 selectionPosition = selection.Get<RingPointsRef>().TargetPoint.transform.position;
        float distance = Vector2.Distance(selectionPosition, originPosition);
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
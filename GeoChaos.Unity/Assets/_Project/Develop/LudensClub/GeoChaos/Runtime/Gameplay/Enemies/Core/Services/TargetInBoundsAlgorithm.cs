using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class TargetInBoundsAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector2 bounds = selection.Get<PatrolBounds>().Bounds;
        if (originPosition.x < bounds.x || originPosition.x > bounds.y)
          selection.Del<Marked>();
      }
    }
  }
}
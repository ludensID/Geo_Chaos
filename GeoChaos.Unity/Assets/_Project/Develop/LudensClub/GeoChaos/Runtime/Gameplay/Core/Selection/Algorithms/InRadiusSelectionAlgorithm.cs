using LudensClub.GeoChaos.Runtime.Gameplay.Ring;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class InRadiusSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly float _radius;

    public InRadiusSelectionAlgorithm(float radius)
    {
      _radius = radius;
    }

    public void Select(EcsEntities origins, EcsEntities selections)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in selections)
      {
        Transform targetTransform = origin.Get<ViewRef>().View.transform;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        if (!selectionTransform.gameObject.activeInHierarchy
          || Vector2.Distance(targetTransform.position, selectionTransform.position) > _radius)
          selection.Del<Selected>();
      }
    }
  }
}
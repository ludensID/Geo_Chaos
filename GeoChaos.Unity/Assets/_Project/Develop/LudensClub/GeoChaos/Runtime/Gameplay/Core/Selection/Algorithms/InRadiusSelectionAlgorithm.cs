﻿using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Selection
{
  public class InRadiusSelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly SelectionData _data;

    public InRadiusSelectionAlgorithm(SelectionData data)
    {
      _data = data;
    }

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Transform targetTransform = origin.Get<ViewRef>().View.transform;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        if (!selectionTransform.gameObject.activeInHierarchy
          || Vector2.Distance(targetTransform.position, selectionTransform.position) > _data.Radius)
          selection.Del<Marked>();
      }
    }
  }
}
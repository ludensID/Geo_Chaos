﻿using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class LamasTargetInViewAlgorithm : ISelectionAlgorithm
  {
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Transform originTransform = origin.Get<ViewRef>().View.transform;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        Vector3 originVector = originTransform.position - selectionTransform.position;
        if (Vector3.Angle(selectionTransform.right, originVector) > 90)
          selection.Del<Marked>();
      }
    }
  }
}
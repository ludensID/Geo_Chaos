﻿using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies
{
  public class ReachedEnemySelectionAlgorithm : ISelectionAlgorithm
  {
    private readonly PhysicsConfig _physics;

    public ReachedEnemySelectionAlgorithm(IConfigProvider configProvider)
    {
      _physics = configProvider.Get<PhysicsConfig>();
    }

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = selection.Get<ViewRef>().View.transform.position;

        Vector3 vector = selectionPosition - originPosition;
        RaycastHit2D centerRaycast = Physics2D.Raycast(originPosition, vector.normalized, vector.magnitude,
          _physics.GroundMask);

        if (centerRaycast.collider != null)
          selection.Del<Marked>();
      }
    }
  }
}
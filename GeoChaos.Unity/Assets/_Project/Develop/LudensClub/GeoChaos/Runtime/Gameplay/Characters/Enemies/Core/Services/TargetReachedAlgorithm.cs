﻿using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  public class TargetReachedAlgorithm : ISelectionAlgorithm
  {
    private readonly PhysicsConfig _physics;
    private readonly List<RaycastHit2D> _hits;
    private readonly ContactFilter2D _filter;

    public TargetReachedAlgorithm(IConfigProvider configProvider)
    {
      _physics = configProvider.Get<PhysicsConfig>();
      
      _hits = new List<RaycastHit2D>(1);
      _filter = new ContactFilter2D
      {
        useTriggers = false,
        useLayerMask = true,
        layerMask = _physics.GroundMask
      };
    }

    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity mark in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Vector3 selectionPosition = mark.Get<ViewRef>().View.transform.position;

        Vector3 vector = selectionPosition - originPosition;
        _hits.Clear();
        bool hasCenterHit = 0 < Physics2D.Raycast(originPosition, vector.normalized, _filter, _hits, vector.magnitude);

        if (hasCenterHit)
          mark.Del<Marked>();
      }
    }
  }
}
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Selection;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Ring
{
  public class ReachedRingAlgorithm : ISelectionAlgorithm
  {
    private readonly PhysicsConfig _physics;
    private readonly List<RaycastHit2D> _hits;
    private readonly ContactFilter2D _filter;

    public ReachedRingAlgorithm(IConfigProvider configProvider)
    {
      _physics = configProvider.Get<PhysicsConfig>();

      _hits = new List<RaycastHit2D>(5);
      _filter = new ContactFilter2D
      {
        useTriggers = true,
      };
    }
    
    public void Select(EcsEntities origins, EcsEntities marks)
    {
      foreach (EcsEntity origin in origins)
      foreach (EcsEntity selection in marks)
      {
        Vector3 originPosition = origin.Get<ViewRef>().View.transform.position;
        Transform selectionTransform = selection.Get<ViewRef>().View.transform;
        Vector3 selectionPosition = selectionTransform.position;
        
        Vector3 vector = selectionPosition - originPosition;
        _hits.Clear();
        bool hasCenterHit = 0 < Physics2D.Raycast(originPosition, vector.normalized, _filter, _hits, vector.magnitude)
          && HasTransform(_hits, selectionTransform);
        
        _hits.Clear();
        bool hasTopHit = 0 < Physics2D.Raycast(originPosition + Vector3.up, vector.normalized,
          _filter, _hits, vector.magnitude) && HasTransform(_hits, selectionTransform);
        
        if (hasCenterHit || hasTopHit)
          selection.Del<Marked>();
      }
    }

    private bool HasTransform(List<RaycastHit2D> hits, Transform match)
    {
      foreach (RaycastHit2D hit in hits)
      {
        if (_physics.GroundMask.Contains(hit.transform.gameObject.layer))
          return false;
          
        if (hit.transform == match)
          return true;
      }

      return false;
    }
  }
}
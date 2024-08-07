﻿using LudensClub.GeoChaos.Runtime.Gameplay.View;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.COLLIDER_CONVERTER)]
  public class ColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _collider;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref ColliderRef colliderRef) => colliderRef.Collider = _collider);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<ColliderRef>();
    }

    private void Reset()
    {
      _collider = GetComponent<Collider2D>();
    }
  }
}
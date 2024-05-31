using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class ColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _collider;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref ColliderRef colliderRef) => colliderRef.Collider = _collider);
    }
    
    private void Reset()
    {
      _collider = GetComponent<Collider2D>();
    }
  }
}
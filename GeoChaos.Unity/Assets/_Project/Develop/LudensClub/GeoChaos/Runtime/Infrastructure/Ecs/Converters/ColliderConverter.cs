using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class ColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _collider;

    public void Convert(EcsWorld world, int entity)
    {
      ref var colliderRef = ref world.Add<ColliderRef>(entity);
      colliderRef.Collider = _collider;
    }
    
    private void Reset()
    {
      _collider = GetComponent<Collider2D>();
    }
  }
}
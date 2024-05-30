using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Converters
{
  public class DashColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _collider;

    public void Convert(EcsWorld world, int entity)
    {
      ref var dashColliderRef = ref world.Add<DashColliderRef>(entity);
      dashColliderRef.Collider = _collider;
      _collider.enabled = false;
    }
  }
}
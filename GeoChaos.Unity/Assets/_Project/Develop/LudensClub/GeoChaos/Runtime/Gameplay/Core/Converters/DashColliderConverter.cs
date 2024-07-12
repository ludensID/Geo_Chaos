using LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Converters
{
  [AddComponentMenu(ACC.Names.DASH_COLLIDER_CONVERTER)]
  public class DashColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _collider;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref DashColliderRef colliderRef) => colliderRef.Collider = _collider);
      _collider.enabled = false;
    }
  }
}
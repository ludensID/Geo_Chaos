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

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref DashColliderRef colliderRef) => colliderRef.Collider = _collider);
      _collider.enabled = false;
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<DashColliderRef>();
    }
  }
}
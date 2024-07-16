using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Immunity
{
  [AddComponentMenu(ACC.Names.IMMUNITY_COLLIDER_CONVERTER)]
  public class ImmunityColliderConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _immunityCollider;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref ImmunityColliderRef colliderRef) => colliderRef.Collider = _immunityCollider);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<ImmunityColliderRef>();
    }
  }
}
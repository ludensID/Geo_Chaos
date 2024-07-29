using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit
{
  [AddComponentMenu(ACC.Names.CALM_COLLIDER_CONVERTER)]
  public class CalmColliderConverter : MonoBehaviour, IEcsConverter
  {
    public Collider2D CalmCollider;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref CalmColliderRef colliderRef) => colliderRef.Collider = CalmCollider);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<CalmColliderRef>();
    }
  }
}
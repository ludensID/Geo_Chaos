using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack
{
  [AddComponentMenu(ACC.Names.ARMS_COLLIDER_CONVERTER)]
  public class ArmsColliderConverter : MonoBehaviour, IEcsConverter
  {
    public Collider2D ArmsCollider;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref ArmsColliderRef armsColliderRef) => armsColliderRef.Collider = ArmsCollider);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<ArmsColliderRef>();
    }
  }
}
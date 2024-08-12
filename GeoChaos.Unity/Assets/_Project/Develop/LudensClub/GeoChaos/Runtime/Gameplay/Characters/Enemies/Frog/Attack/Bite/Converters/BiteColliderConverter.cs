using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Bite
{
  [AddComponentMenu(ACC.Names.BITE_COLLIDER_CONVERTER)]
  public class BiteColliderConverter : MonoBehaviour, IEcsConverter
  {
    public Collider2D Collider;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref BiteColliderRef colliderRef) => colliderRef.Collider = Collider);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<BiteColliderRef>();
    }
  }
}
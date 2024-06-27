using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama.View
{
  public class LamaAttackCollidersConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Collider2D _hitCollider;

    [SerializeField]
    private Collider2D _comboCollider;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref LamaAttackCollidersRef colliders) =>
      {
        colliders.HitCollider = _hitCollider;
        colliders.ComboCollider = _comboCollider;
      });
    }
  }
}
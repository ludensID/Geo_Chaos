using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.RIGIDBODY_CONVERTER)]
  public class RigidbodyConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Rigidbody2D _rigidbody;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref RigidbodyRef rigidbodyRef) => rigidbodyRef.Rigidbody = _rigidbody);
    }

    private void Reset()
    {
      _rigidbody = GetComponent<Rigidbody2D>();
    }
  }
}
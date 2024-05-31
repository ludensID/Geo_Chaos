using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
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
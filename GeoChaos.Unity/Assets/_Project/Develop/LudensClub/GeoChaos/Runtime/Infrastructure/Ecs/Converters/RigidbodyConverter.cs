using LudensClub.GeoChaos.Runtime.Gameplay.View;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  [AddComponentMenu(ACC.Names.RIGIDBODY_CONVERTER)]
  public class RigidbodyConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Rigidbody2D _rigidbody;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref RigidbodyRef rigidbodyRef) => rigidbodyRef.Rigidbody = _rigidbody);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<RigidbodyRef>();
    }

    private void Reset()
    {
      _rigidbody = GetComponent<Rigidbody2D>();
    }
  }
}
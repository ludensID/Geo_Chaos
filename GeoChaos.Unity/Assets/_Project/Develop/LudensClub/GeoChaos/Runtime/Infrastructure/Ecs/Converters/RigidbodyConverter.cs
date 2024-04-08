using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class RigidbodyConverter : MonoBehaviour, IEcsConverter
  {
    [SerializeField]
    private Rigidbody2D _rigidbody;

    public void Convert(EcsWorld world, int entity)
    {
      ref var rigidbodyRef = ref world.Add<RigidbodyRef>(entity);
      rigidbodyRef.Rigidbody = _rigidbody;
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Converters
{
  public class GroundCheckConverter : MonoBehaviour, IEcsConverter
  {
    public Transform Bottom;

    public void Convert(EcsWorld world, int entity)
    {
      ref var groundCheckRef = ref world.Add<GroundCheckRef>(entity);
      groundCheckRef.Bottom = Bottom;
    }
  }
}
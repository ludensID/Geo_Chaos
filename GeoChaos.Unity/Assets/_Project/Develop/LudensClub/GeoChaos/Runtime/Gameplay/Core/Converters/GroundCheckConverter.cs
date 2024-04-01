using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Infrastructure.Converters
{
  public class GroundCheckConverter : MonoBehaviour, IEcsConverter
  {
    public Transform Bottom;
    
    public void Convert(EcsWorld world, int entity)
    {
      ref GroundCheckRef groundCheckRef = ref world.Add<GroundCheckRef>(entity);
      groundCheckRef.Bottom = Bottom;
    }
  }
}
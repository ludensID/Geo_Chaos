using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  public class RingPointsConverter : MonoBehaviour, IEcsConverter
  {
    public Transform TargetPoint;
    
    public void Convert(EcsWorld world, int entity)
    {
      ref RingPoints points = ref world.Add<RingPoints>(entity);
      points.TargetPoint = TargetPoint;
    }
  }
}
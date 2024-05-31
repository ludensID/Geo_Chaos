using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  public class RingPointsConverter : MonoBehaviour, IEcsConverter
  {
    public Transform TargetPoint;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref RingPoints points) => points.TargetPoint = TargetPoint);
    }
  }
}
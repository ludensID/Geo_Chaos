using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Ring
{
  [AddComponentMenu(ACC.Names.RING_POINTS_CONVERTER)]
  public class RingPointsConverter : MonoBehaviour, IEcsConverter
  {
    public Transform TargetPoint;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref RingPointsRef points) => points.TargetPoint = TargetPoint);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<RingPointsRef>();
    }
  }
}
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.GasShooting
{
  public class GasCloudPointConverter : MonoBehaviour, IEcsConverter
  {
    public Transform GasCloudPoint;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref GasCloudPointRef gasCloudPointRef) => gasCloudPointRef.Point = GasCloudPoint);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<GasCloudPointRef>();
    }
  }
}
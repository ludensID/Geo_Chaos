using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  [AddComponentMenu(ACC.Names.TONGUE_POINT_CONVERTER)]
  public class TonguePointConverter : MonoBehaviour, IEcsConverter
  {
    public Transform TonguePoint;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref TonguePointRef tonguePointRef) => tonguePointRef.Point = TonguePoint);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<TonguePointRef>();
    }
  }
}
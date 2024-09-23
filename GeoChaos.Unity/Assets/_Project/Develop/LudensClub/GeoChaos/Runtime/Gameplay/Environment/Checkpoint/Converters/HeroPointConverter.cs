using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Checkpoint
{
  [AddComponentMenu(ACC.Names.HERO_POINT_CONVERTER)]
  public class HeroPointConverter : MonoBehaviour, IEcsConverter
  {
    public Transform Point;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref HeroPointRef point) => point.Point = Point);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<HeroPointRef>();
    }
  }
}
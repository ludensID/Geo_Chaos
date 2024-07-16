using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HERO_SHOOT_LINE_CONVERTER)]
  public class HeroShootLineConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer ShootLine;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref ShootLineRef lineRef) => lineRef.Line = ShootLine);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<ShootLineRef>();
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  public class HeroShootLineConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer ShootLine;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref ShootLineRef lineRef) => lineRef.Line = ShootLine);
    }
  }
}
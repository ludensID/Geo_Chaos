using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  public class HeroShootLineConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer ShootLine;
    
    public void Convert(EcsWorld world, int entity)
    {
      ref ShootLineRef line = ref world.Add<ShootLineRef>(entity);
      line.Line = ShootLine;
    }
  }
}
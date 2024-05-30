using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  public class HeroSwordViewConverter : MonoBehaviour, IEcsConverter
  {
    public HeroSwordView View;
    
    public void Convert(EcsWorld world, int entity)
    {
      ref HeroSwordViewRef viewRef = ref world.Add<HeroSwordViewRef>(entity);
      viewRef.View = View;
    }
  }
}
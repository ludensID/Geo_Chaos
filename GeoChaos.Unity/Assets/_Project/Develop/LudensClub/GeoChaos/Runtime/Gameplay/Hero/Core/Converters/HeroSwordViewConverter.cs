using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HERO_SWORD_VIEW_CONVERTER)]
  public class HeroSwordViewConverter : MonoBehaviour, IEcsConverter
  {
    public HeroSwordView View;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref HeroSwordViewRef swordRef) => swordRef.View = View);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<HeroSwordViewRef>();
    }
  }
}
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies
{
  [AddComponentMenu(ACC.Names.HEALTH_CONVERTER)]
  public class HealthViewConverter : MonoBehaviour, IEcsConverter
  {
    public HealthView View;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref HealthRef healthRef) => healthRef.View = View);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<HealthRef>();
    }
  }
}
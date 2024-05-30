using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemy
{
  public class HealthConverter : MonoBehaviour, IEcsConverter
  {
    public HealthView View;
    
    public void Convert(EcsWorld world, int entity)
    {
      world.Add<HealthRef>(entity).View = View;
    }
  }
}
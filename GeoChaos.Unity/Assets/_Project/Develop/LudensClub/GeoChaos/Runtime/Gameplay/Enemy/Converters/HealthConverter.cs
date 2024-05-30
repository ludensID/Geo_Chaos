using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.UI;
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
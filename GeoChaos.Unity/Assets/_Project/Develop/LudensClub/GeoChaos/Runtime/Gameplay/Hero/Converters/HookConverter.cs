using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  public class HookConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer Hook;
    
    public void Convert(EcsWorld world, int entity)
    {
      ref HookRef hookRef = ref world.Add<HookRef>(entity);
      hookRef.Hook = Hook;
    }
  }
}
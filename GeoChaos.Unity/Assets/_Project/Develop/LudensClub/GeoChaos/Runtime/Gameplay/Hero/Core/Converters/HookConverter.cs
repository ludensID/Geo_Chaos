using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HOOK_CONVERTER)]
  public class HookConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer Hook;
    
    public void Convert(EcsEntity entity)
    {
      entity.Add((ref HookRef hookRef) => hookRef.Hook = Hook);
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Hook;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Converters
{
  [AddComponentMenu(ACC.Names.HOOK_CONVERTER)]
  public class HookConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer Hook;
    
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref HookRef hookRef) => hookRef.Hook = Hook);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<HookRef>();
    }
  }
}
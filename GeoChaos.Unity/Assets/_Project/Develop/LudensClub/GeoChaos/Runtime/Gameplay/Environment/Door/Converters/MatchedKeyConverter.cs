using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  [AddComponentMenu(ACC.Names.MATCHED_KEY_CONVERTER)]
  public class MatchedKeyConverter : MonoBehaviour, IEcsConverter
  {
    public BaseView MatchedKey;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref KeyViewRef keyRef) => keyRef.Key = MatchedKey);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<KeyViewRef>();
    }
  }
}
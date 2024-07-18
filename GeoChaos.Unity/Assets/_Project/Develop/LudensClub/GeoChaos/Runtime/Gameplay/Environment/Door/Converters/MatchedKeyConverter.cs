using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using LudensClub.GeoChaos.Runtime.Props.Environment.Key;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door
{
  [AddComponentMenu(ACC.Names.MATCHED_KEY_CONVERTER)]
  public class MatchedKeyConverter : MonoBehaviour, IEcsConverter
  {
    public KeyView MatchedKey;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref MatchedKeyRef keyRef) => keyRef.Key = MatchedKey);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<MatchedKeyRef>();
    }
  }
}
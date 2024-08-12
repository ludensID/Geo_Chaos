using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Frog.Attack.Tongue
{
  [AddComponentMenu(ACC.Names.TONGUE_LINE_CONVERTER)]
  public class TongueLineConverter : MonoBehaviour, IEcsConverter
  {
    public LineRenderer TongueLine;
      
    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref TongueLineRef lineRef) => lineRef.Line = TongueLine);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<TongueLineRef>();
    }
  }
}
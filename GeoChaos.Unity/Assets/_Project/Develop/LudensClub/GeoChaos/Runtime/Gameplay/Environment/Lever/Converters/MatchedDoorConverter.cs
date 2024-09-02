using LudensClub.GeoChaos.Runtime.Gameplay.Environment.Door;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Lever
{
  [AddComponentMenu(ACC.Names.MATCHED_DOOR_CONVERTER)]
  public class MatchedDoorConverter : MonoBehaviour, IEcsConverter
  {
    public DoorView Door;


    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref MatchedDoorRef doorRef) => doorRef.Door = Door);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<MatchedDoorRef>();
    }
  }
}
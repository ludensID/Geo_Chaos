using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Gravity;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Converters
{
  [AddComponentMenu(ACC.Names.GROUND_CHECK_CONVERTER)]
  public class GroundCheckConverter : MonoBehaviour, IEcsConverter
  {
    public Transform Bottom;

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref GroundCheckRef groundRef) => groundRef.Bottom = Bottom);
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<GroundCheckRef>();
    }
  }
}
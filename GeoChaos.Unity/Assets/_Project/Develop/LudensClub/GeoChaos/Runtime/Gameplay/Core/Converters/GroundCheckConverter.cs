using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Converters
{
  [AddComponentMenu(ACC.Names.GROUND_CHECK_CONVERTER)]
  public class GroundCheckConverter : MonoBehaviour, IEcsConverter
  {
    public Transform Bottom;

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref GroundCheckRef groundRef) => groundRef.Bottom = Bottom);
    }
  }
}
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  [AddComponentMenu(ACC.Names.PHYSICAL_BOUNDS_CONVERTER)]
  public class PhysicalBoundsConverter : MonoBehaviour, IEcsConverter
  {
    [InfoBox("The left bound can not be more than the right one", TriMessageType.Error,
      TriConstants.Names.Explicit.CHECK_BOUNDS)]
    public Transform LeftBound;

    public Transform RightBound;

    public static Rect GetBounds(Transform left, Transform right)
    {
      var rect = new Rect
      {
        min = GetBound(left, Vector2.negativeInfinity),
        max = GetBound(right, Vector2.positiveInfinity)
      };
      
      return rect;

      Vector2 GetBound(Transform bound, Vector2 defaultValue)
      {
        return bound ? bound.position : defaultValue;
      }
    }

    public void ConvertTo(EcsEntity entity)
    {
      entity.Add((ref PhysicalBoundsRef bounds) =>
      {
        bounds.Left = LeftBound;
        bounds.Right = RightBound;
      });
    }

    public void ConvertBack(EcsEntity entity)
    {
      entity.Del<PhysicalBoundsRef>();
    }

#if UNITY_EDITOR
    private Rect Bounds => GetBounds(LeftBound, RightBound);

    [ShowInInspector]
    private Vector2 HorizontalBounds => new Vector2(Bounds.xMin, Bounds.xMax);

    [ShowInInspector]
    private Vector2 VerticalBounds => new Vector2(Bounds.yMin, Bounds.yMax);

    private bool CheckBounds()
    {
      return LeftBound && RightBound && LeftBound.position.x > RightBound.position.x;
    }
#endif
  }
}
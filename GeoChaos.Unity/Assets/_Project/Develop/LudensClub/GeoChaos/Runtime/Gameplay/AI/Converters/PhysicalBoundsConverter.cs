using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using TriInspector;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI
{
  public class PhysicalBoundsConverter : MonoBehaviour, IEcsConverter
  {
    [InfoBox("The left bound can not be more than the right one", TriMessageType.Error,
      TriConstants.Names.Explicit.CHECK_BOUNDS)]
    public Transform LeftBound;
    public Transform RightBound;

    public static Vector2 GetBounds(Transform left, Transform right)
    {
      return new Vector2(GetBound(left, float.MinValue), GetBound(right, float.MaxValue));

      float GetBound(Transform bound, float defaultValue)
      {
        return bound ? bound.position.x : defaultValue;
      }
    }

    public void Convert(EcsEntity entity)
    {
      entity.Add((ref PhysicalBoundsRef bounds) =>
      {
        bounds.Left = LeftBound;
        bounds.Right = RightBound;
      });
    }

#if UNITY_EDITOR
    [ShowInInspector]
    private Vector2 Bounds => GetBounds(LeftBound, RightBound);

    private bool CheckBounds()
    {
      return LeftBound && RightBound && LeftBound.position.x > RightBound.position.x;
    }

    private void OnDrawGizmos()
    {
      Color color = Color.blue;
      color.a = 0.2f;
      Gizmos.color = color;
      
      var center = transform.position;
      center.x = (LeftBound.position.x + RightBound.position.x) / 2;
      var size = new Vector3(RightBound.position.x - LeftBound.position.x, 3, 3);
      Gizmos.DrawCube(center, size);
    }
#endif
  }
}
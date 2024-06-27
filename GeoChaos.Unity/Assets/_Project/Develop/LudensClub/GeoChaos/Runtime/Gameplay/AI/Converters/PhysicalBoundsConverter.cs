using System.Linq;
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
      Transform active = UnityEditor.Selection.activeTransform;
      if (!active || !active.IsChildOf(transform))
        return;
      
      RaycastHit2D[] results = new RaycastHit2D[5];
      var filter = new ContactFilter2D
      {
        layerMask = LayerMask.GetMask("Ground"),
        useLayerMask = true,
        useTriggers = false
      };
      int hitCounts = Physics2D.Raycast(transform.position, Vector2.down, filter, results, 3);
      Vector3 origin = hitCounts > 0 ? results.First().point : transform.position;
      
      Color color = Color.blue;
      color.a = 0.2f;
      Gizmos.color = color;
      
      var size = new Vector3(RightBound.position.x - LeftBound.position.x, 3, 3);
      var center = transform.position;
      center.x = (LeftBound.position.x + RightBound.position.x) / 2;
      center.y = origin.y + size.y / 2;
      Gizmos.DrawCube(center, size);
    }
#endif
  }
}
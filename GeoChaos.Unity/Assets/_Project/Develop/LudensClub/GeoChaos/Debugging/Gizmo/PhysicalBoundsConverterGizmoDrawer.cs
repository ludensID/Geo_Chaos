using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Gizmo
{
  public static class PhysicalBoundsConverterGizmoDrawer
  {
    public static void DrawGizmoImplicit(PhysicalBoundsConverter src)
    {
      if (!src.RightBound || !src.LeftBound)
        return;
      
      if (!_physics)
        _physics = AssetFinder.FindAsset<PhysicsConfig>();

      var raycastData = new RaycastData
      {
        Position = src.transform.position,
        Filter = new ContactFilter2D
        {
          useLayerMask = true,
          layerMask = _physics.GroundMask,
          useTriggers = false
        }
      };

      var hits = new RaycastHit2D[1];
      var rect = new Rect();

      raycastData.Direction = Vector2.left;
      raycastData.Distance = GetDistance(raycastData.Position.x, src.LeftBound.position.x);
      rect.xMin = Raycast(raycastData, hits, 0, src.LeftBound.position.x);
      
      raycastData.Direction = Vector2.down;
      raycastData.Distance = GetDistance(raycastData.Position.y, src.LeftBound.position.y);
      rect.yMin = Raycast(raycastData, hits, 1, src.LeftBound.position.y);

      raycastData.Direction = Vector2.right;
      raycastData.Distance = GetDistance(raycastData.Position.x, src.RightBound.position.x);
      rect.xMax = Raycast(raycastData, hits, 0, src.RightBound.position.x);

      raycastData.Direction = Vector2.up;
      raycastData.Distance = GetDistance(raycastData.Position.y, src.RightBound.position.y);
      rect.yMax = Raycast(raycastData, hits, 1, src.RightBound.position.y);

      Color color = Color.blue;
      color.a = 0.5f;
      Gizmos.color = color;
      
      Gizmos.DrawCube(rect.center, rect.size);
    }

    private static PhysicsConfig _physics;

    private static float GetDistance(float a, float b)
    {
      return Mathf.Abs(a - b);
    }

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    private static void DrawGizmo(PhysicalBoundsConverter src, GizmoType gizmo)
    {
      if (!SelectionHelper.IsSelectionOrChild(src.transform))
        return;

      DrawGizmoImplicit(src);
    }

    private static float Raycast(RaycastData raycastData, RaycastHit2D[] hits, int index, float defaultValue)
    {
      int count = Physics2D.Raycast(raycastData.Position, raycastData.Direction, raycastData.Filter, hits, raycastData.Distance);
      return count > 0 ? hits[0].point[index] : defaultValue;
    }

    private struct RaycastData
    {
      public Vector2 Position;
      public Vector2 Direction;
      public float Distance;
      public ContactFilter2D Filter;
    }
  }
}
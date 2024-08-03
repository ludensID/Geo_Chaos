using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Gizmo
{
  public static class PhysicalBoundsConverterGizmoDrawer
  {
    private static PhysicsConfig _physics;

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
    private static void DrawGizmo(PhysicalBoundsConverter src, GizmoType gizmo)
    {
      if (!SelectionHelper.IsSelectionOrChild(src.transform))
        return;

      DrawGizmoImplicit(src);
    }
    
    public static void DrawGizmoImplicit(PhysicalBoundsConverter src)
    {
      if (!src.RightBound || !src.LeftBound)
        return;
      
      if (!_physics)
        _physics = AssetFinder.FindAsset<PhysicsConfig>();
        
      RaycastHit2D hit = Physics2D.Raycast(src.transform.position, Vector2.down, 3, _physics.GroundMask);
      Vector3 origin = hit.collider ? hit.point : src.transform.position;

      Color color = Color.blue;
      color.a = 0.5f;
      Gizmos.color = color;

      var size = new Vector3(src.RightBound.position.x - src.LeftBound.position.x, 3, 3);
      var center = src.transform.position;
      center.x = (src.LeftBound.position.x + src.RightBound.position.x) / 2;
      center.y = origin.y + size.y / 2;
      Gizmos.DrawCube(center, size);
    }
  }
}
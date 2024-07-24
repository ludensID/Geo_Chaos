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
    private static void DrawGizmo(PhysicalBoundsConverter scr, GizmoType gizmo)
    {
      if (!SelectionHelper.IsSelectionOrChild(scr.transform))
        return;
      
      if (!scr.RightBound || !scr.LeftBound)
        return;
      
      if (!_physics)
        _physics = AssetFinder.FindAsset<PhysicsConfig>();

      RaycastHit2D hit = Physics2D.Raycast(scr.transform.position, Vector2.down, 3, _physics.GroundMask);
      Vector3 origin = hit.collider ? hit.point : scr.transform.position;

      Color color = Color.blue;
      color.a = 0.5f;
      Gizmos.color = color;

      var size = new Vector3(scr.RightBound.position.x - scr.LeftBound.position.x, 3, 3);
      var center = scr.transform.position;
      center.x = (scr.LeftBound.position.x + scr.RightBound.position.x) / 2;
      center.y = origin.y + size.y / 2;
      Gizmos.DrawCube(center, size);
    }
  }
}
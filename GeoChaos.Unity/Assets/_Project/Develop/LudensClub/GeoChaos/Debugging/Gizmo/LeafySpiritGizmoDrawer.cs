using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Props.LeafySpirit;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging.Gizmo
{
  public static class LeafySpiritGizmoDrawer
  {
    private static LeafySpiritConfig _config;
    private static PhysicsConfig _physics;

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(LeafySpiritGizmo))]
    private static void DrawGizmo(LeafySpiritGizmo src, GizmoType gizmo)
    {
      if (!SelectionHelper.IsSelectionOrChild(src.transform))
        return;
      
      if (!_config)
        _config = AssetFinder.FindAsset<LeafySpiritConfig>();
      if (!_physics)
        _physics = AssetFinder.FindAsset<PhysicsConfig>();
      
      var bounds = src.GetComponentInParent<PhysicalBoundsConverter>();
      if (!bounds)
        return;
      
      RaycastHit2D hit = Physics2D.Raycast(src.transform.position, Vector2.down, 3, _physics.GroundMask);
      Vector3 origin = hit.collider ? hit.point : src.transform.position;
      var leftBottom = new Vector3(bounds.LeftBound.position.x - _config.AttackDistance, origin.y,
        src.transform.position.z);
      var rightTop = new Vector3(bounds.RightBound.position.x + _config.AttackDistance,
        origin.y + _config.MaxVerticalDistance, src.transform.position.z);
      
      Color color = Color.green;
      color.a = 0.5f;
      Gizmos.color = color;
      Gizmos.DrawCube((rightTop + leftBottom) / 2, rightTop - leftBottom);
    }
  }
}
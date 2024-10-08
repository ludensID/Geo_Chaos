using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Infrastructure.Converters;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Gizmo
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
      
      if (!TryGetPhysicalBounds(src, out PhysicalBoundsRef bounds))
        return;
      
      RaycastHit2D hit = Physics2D.Raycast(src.transform.position, Vector2.down, 3, _physics.GroundMask);
      Vector3 origin = hit.collider ? hit.point : src.transform.position;
      var leftBottom = new Vector3(bounds.Left.position.x - _config.AttackDistance, origin.y,
        src.transform.position.z);
      var rightTop = new Vector3(bounds.Right.position.x + _config.AttackDistance,
        bounds.Right.position.y, src.transform.position.z);
      
      Color color = Color.green;
      color.a = 0.5f;
      Gizmos.color = color;
      Gizmos.DrawCube((rightTop + leftBottom) / 2, rightTop - leftBottom);
    }

    private static bool TryGetPhysicalBounds(LeafySpiritGizmo src, out PhysicalBoundsRef bounds)
    {
      bounds = new PhysicalBoundsRef();
      var converter = src.GetComponent<PhysicalBoundsConverter>();
      if (converter)
      {
        bounds.Left = converter.LeftBound;
        bounds.Right = converter.RightBound;
        return true;
      }

      if (EditorApplication.isPlaying)
      {
        var goConverter = src.GetComponent<GameObjectConverter>();
        if (goConverter && goConverter.Entity.IsAlive())
        {
          if (goConverter.Entity.Get<Spawned>().Spawn.TryUnpackEntity(goConverter.Entity.World, out EcsEntity spawn))
          {
            PhysicalBoundsConverterGizmoDrawer.DrawGizmoImplicit(spawn.Get<ViewRef>().View.GetComponent<PhysicalBoundsConverter>());
          }
          bounds = goConverter.Entity.Get<PhysicalBoundsRef>();
          return true;
        }
      }

      return false;
    }
  }
}
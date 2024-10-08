using LudensClub.GeoChaos.Editor.General;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama;
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Editor.Gizmo
{
  public static class LamaGizmoDrawer
  {
    private static LamaConfig _config;
    private static PhysicsConfig _physics;

    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected, typeof(LamaGizmo))]
    private static void DrawGizmo(LamaGizmo src, GizmoType gizmo)
    {
      if (!SelectionHelper.IsSelectionOrChild(src.transform))
        return;
      
      if (!_config)
        _config = AssetFinder.FindAsset<LamaConfig>();
      if (!_physics)
        _physics = AssetFinder.FindAsset<PhysicsConfig>();
      
      RaycastHit2D hit = Physics2D.Raycast(src.transform.position, Vector2.down, 3, _physics.GroundMask);
      Vector3 origin = hit.collider ? hit.point : src.transform.position;
      var mesh = new Mesh();
      int count = 9;
      float step = Mathf.PI / 2f / (count - 1);
      Vector3[] vertices = new Vector3[count + 1];
      int[] triangles = new int[count * 3];
      int i = 0;
      int k = 0;
      vertices[i++] = Vector3.zero;
      var scale = Mathf.Sign(src.transform.right.x);
      for (float alpha = 0; alpha <= Mathf.PI / 2; alpha += step, i++)
      {
        var direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha));
        direction.x *= scale;
        vertices[i] = direction * _config.ViewRadius;
        
        if (i >= 2)
        {
          int a = i, b = i - 1;
          if (scale < 0)
            (a, b) = (b, a);
          triangles[k++] = 0;
          triangles[k++] = a;
          triangles[k++] = b;
        }
      }

      mesh.vertices = vertices;
      mesh.triangles = triangles;
      mesh.RecalculateNormals();

      Color color = Color.yellow;
      color.a = 0.5f;
      Gizmos.color = color;
      Gizmos.DrawMesh(mesh, origin);
    }
  }
}

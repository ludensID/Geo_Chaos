using System.Linq;
using LudensClub.GeoChaos.Runtime.AI;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Props.Enemies.Lama
{
  public class LamaContextView : BrainContextView
  {
    [SerializeField]
    private LamaContext _context;

    public override IBrainContext Context
    {
      get => _context;
      set => _context = (LamaContext)value;
    }

#if UNITY_EDITOR
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
      var mesh = new Mesh();
      int count = 9;
      float step = Mathf.PI / 2f / (count - 1);
      Vector3[] vertices = new Vector3[count + 1];
      int[] triangles = new int[count * 3];
      int i = 0;
      int k = 0;
      vertices[i++] = Vector3.zero;
      var scale = Mathf.Sign(transform.right.x);
      for (float alpha = 0; alpha <= Mathf.PI / 2; alpha += step, i++)
      {
        var direction = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha));
        direction.x *= scale;
        vertices[i] = direction * _context.ViewRadius;
        
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
#endif
  }

  public abstract class BrainContextView : MonoBehaviour, IBrainContextView
  {
    public abstract IBrainContext Context { get; set; }
  }

  public interface IBrainContextView
  {
    public IBrainContext Context { get; }
  }
}
using UnityEditor;
using UnityEngine;

namespace LudensClub.GeoChaos.Debugging
{
  [ExecuteAlways]
  [AddComponentMenu(ACC.Names.ATTACK_COLLIDER_MESH_MENU)]
  public class AttackColliderMesh : MonoBehaviour
  {
    public Color Color;
    public PolygonCollider2D Collider;
    public MeshFilter MeshFilter;
    public MeshRenderer MeshRenderer;

    private void Start()
    {
      Collider = GetComponent<PolygonCollider2D>();
      if (!gameObject.TryGetComponent(out MeshFilter))
        MeshFilter = gameObject.AddComponent<MeshFilter>();
      if (!gameObject.TryGetComponent(out MeshRenderer))
        MeshRenderer = gameObject.AddComponent<MeshRenderer>();
      MeshRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    private void Update()
    {
      Mesh mesh = GetMesh();
      if (mesh)
      {
        Vector3[] vertices = mesh.vertices;
        Matrix4x4 matrix = transform.localToWorldMatrix.inverse;
        for (int i = 0; i < vertices.Length; i++)
        {
          vertices[i] = matrix.MultiplyPoint(vertices[i]);
        }

        mesh.vertices = vertices;

        MeshRenderer.sharedMaterial.color = Color;
        MeshFilter.mesh = mesh;
      }
    }

    private Mesh GetMesh()
    {
      bool enable = Collider.enabled;
      if (!EditorApplication.isPlaying)
        Collider.enabled = true;
      Mesh mesh = Collider.CreateMesh(true, true);
      if (!EditorApplication.isPlaying)
        Collider.enabled = enable;
      return mesh;
    }
  }
}
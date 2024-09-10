using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class UnityExtensions
  {
    public static int GetLayerIndex(this LayerMask obj)
    {
      var index = 0;
      int layer = obj.value;
      while (layer > 1)
      {
        layer >>= 1;
        ++index;
      }

      return index;
    }

    public static bool Contains(this LayerMask mask, int layer)
    {
      return (mask & (1 << layer)) > 0;
    }

    public static void SetIndex(this ref Vector2 obj, int index, float value)
    {
      obj[index] = value;
    }

    public static void SetX(this ref Vector2 obj, float x)
    {
      obj.SetIndex(0, x);
    }

    public static void SetY(this ref Vector2 obj, float y)
    {
      obj.SetIndex(1, y);
    }

    public static void SetIndex(this ref Vector3 obj, int index, float value)
    {
      obj[index] = value;
    }

    public static void SetX(this ref Vector3 obj, float x)
    {
      obj.SetIndex(0, x);
    }

    public static void SetY(this ref Vector3 obj, float y)
    {
      obj.SetIndex(1, y);
    }

    public static void SetZ(this ref Vector3 obj, float z)
    {
      obj.SetIndex(2, z);
    }
  }
}
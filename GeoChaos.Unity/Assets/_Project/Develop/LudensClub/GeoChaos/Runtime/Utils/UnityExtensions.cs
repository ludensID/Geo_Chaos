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
  }
}
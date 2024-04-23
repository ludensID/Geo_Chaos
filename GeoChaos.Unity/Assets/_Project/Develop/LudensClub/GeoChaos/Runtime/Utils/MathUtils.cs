using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class MathUtils
  {
    public static float Clamp(float value, float min = float.MinValue, float max = float.MaxValue)
    {
      return Mathf.Clamp(value, min, max);
    }
  }
}
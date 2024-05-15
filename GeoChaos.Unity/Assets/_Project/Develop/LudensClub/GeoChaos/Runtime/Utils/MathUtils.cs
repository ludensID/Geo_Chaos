using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class MathUtils
  {
    public static float Clamp(float value, float min = float.MinValue, float max = float.MaxValue)
    {
      return Mathf.Clamp(value, min, max);
    }
    
    public static float DecreaseToZero(float value, float delta)
    {
      value -= delta;
      value = Clamp(value, 0);
      return value;
    }

    public static (Vector3 length, Vector3 direction) DecomposeVector(Vector3 vector)
    {
      var length = new Vector3();
      var direction = new Vector3();
      for (int i = 0; i < 3; i++)
      {
        length[i] = Mathf.Abs(vector[i]);
        direction[i] = Mathf.Sign(vector[i]);
      }

      return (length, direction);
    }
  }
}
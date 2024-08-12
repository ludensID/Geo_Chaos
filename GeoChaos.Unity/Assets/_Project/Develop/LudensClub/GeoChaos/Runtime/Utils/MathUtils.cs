using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class MathUtils
  {
    private const float TOLERANCE = 0.000001f;

    public static bool ApproximatelyEqual(this float left, float right, float tolerance = TOLERANCE)
    {
      return Mathf.Abs(left - right) <= tolerance;
    }
    
    public static bool ApproximatelyEqual(this Vector2 left, Vector2 right, float tolerance = TOLERANCE)
    {
      return ((Vector3)left).ApproximatelyEqual(right, tolerance);
    }

    public static bool ApproximatelyEqual(this Vector3 left, Vector3 right, float tolerance = TOLERANCE)
    {
      return (right - left).sqrMagnitude <= Mathf.Pow(tolerance, 2);
    }
      
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
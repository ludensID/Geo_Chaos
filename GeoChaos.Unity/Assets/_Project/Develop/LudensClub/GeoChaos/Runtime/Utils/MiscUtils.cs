using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class MiscUtils
  {
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
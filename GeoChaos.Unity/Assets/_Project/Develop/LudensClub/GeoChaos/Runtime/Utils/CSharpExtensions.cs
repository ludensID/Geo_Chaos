using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class CSharpExtensions
  {
    private const float TOLERANCE = 0.000001f;

    public static bool ApproximatelyEqual(this float left, float right, float tolerance = TOLERANCE)
    {
      return Math.Abs(left - right) <= tolerance;
    }

    public static bool AllNonAlloc<T>(this List<T> obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Predicate(p))
          return false;
      }

      return true;
    }

    public static bool AllNonAlloc<T>(this T[] obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Predicate(p))
          return false;
      }

      return true;
    }

    public static bool AnyNonAlloc<T>(this List<T> obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (predicate.Predicate(p))
          return true;
      }

      return false;
    }

    public static bool AnyNonAlloc<T>(this T[] obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (predicate.Predicate(p))
          return true;
      }

      return false;
    }
  }
}
using System;
using System.Collections.Generic;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class CSharpExtensions
  {
    public static bool AllNonAlloc<T>(this List<T> obj, Predicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Invoke(p))
          return false;
      }

      return true;
    }

    public static bool AllNonAlloc<T>(this T[] obj, Predicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Invoke(p))
          return false;
      }

      return true;
    }

    public static bool AnyNonAlloc<T>(this List<T> obj, Predicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (predicate.Invoke(p))
          return true;
      }

      return false;
    }

    public static bool AnyNonAlloc<T>(this T[] obj, Predicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (predicate.Invoke(p))
          return true;
      }

      return false;
    }
    
    public static int FindIndexNonAlloc<T>(this List<T> obj, Predicate<T> predicate)
    {
      for (int i = 0; i < obj.Count; i++)
      {
        if (predicate.Invoke(obj[i]))
          return i;
      }

      return -1;
    }

    public static T FindNonAlloc<T>(this List<T> obj, Predicate<T> predicate)
    {
      foreach (T value in obj)
      {
        if (predicate.Invoke(value))
          return value;
      }

      return default(T);
    }
  }
}
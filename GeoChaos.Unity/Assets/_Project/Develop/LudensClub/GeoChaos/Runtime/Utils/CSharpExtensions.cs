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
  }
}
using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Utils
{
  public static class CSharpExtensions
  {
    public static bool AllNonAlloc<T>(this List<T> obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Predicate(p))
          return false;
      }

      return true;
    }
    
    public static bool AllNonAlloc<T>(T[] obj, IPredicate<T> predicate)
    {
      foreach (T p in obj)
      {
        if (!predicate.Predicate(p))
          return false;
      }

      return true;
    }
  }
}
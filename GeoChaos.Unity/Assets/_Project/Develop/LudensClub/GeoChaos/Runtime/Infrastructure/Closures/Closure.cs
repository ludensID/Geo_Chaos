using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class Closure<TType>
  {
    public Predicate<TType> Predicate;
  }
}
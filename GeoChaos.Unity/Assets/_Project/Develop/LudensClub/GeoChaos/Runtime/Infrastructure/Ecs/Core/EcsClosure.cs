using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public abstract class EcsClosure<TType>
  {
    public readonly Predicate<TType> Predicate;

    protected EcsClosure()
    {
      Predicate = Call;
    }

    protected abstract bool Call(TType value);
  }
}
using System;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class SpecifiedClosure<TType, TData> : InternalClosure<TType>
  {
    public TData Data;
    public Func<TType, TData, bool> SpecifiedPredicate;
      
    public SpecifiedClosure()
    {
    }

    public SpecifiedClosure(Func<TType, TData, bool> predicate)
    {
      SpecifiedPredicate = predicate;
    }

    public Predicate<TType> SpecifyPredicate(TData data)
    {
      Data = data;
      return Predicate;
    }

    protected override bool Call(TType value)
    {
      return SpecifiedPredicate.Invoke(value, Data);
    }
  }
}
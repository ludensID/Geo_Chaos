namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public abstract class SpecifiedInternalClosure<TType, TData> : SpecifiedClosure<TType, TData>
  {
    protected SpecifiedInternalClosure()
    {
      SpecifiedPredicate = Call;
    }
    
    protected sealed override bool Call(TType value)
    {
      return SpecifiedPredicate.Invoke(value, Data);
    }

    protected abstract bool Call(TType value, TData data);
  }
}
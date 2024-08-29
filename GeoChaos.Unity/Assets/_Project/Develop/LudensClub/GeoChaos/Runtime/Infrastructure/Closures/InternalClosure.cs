namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public abstract class InternalClosure<TType> : Closure<TType>
  {
    protected InternalClosure()
    {
      Predicate = Call;
    }

    protected abstract bool Call(TType value);
  }
}
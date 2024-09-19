using System;
using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class ExplicitInitializer : IExplicitInitializer
  {
    private readonly InitializableManager _initializer;
    private readonly TickableManager _ticker;
    private readonly DisposableManager _disposer;

    public ExplicitInitializer(InitializableManager initializer, TickableManager ticker, DisposableManager disposer)
    {
      _initializer = initializer;
      _ticker = ticker;
      _disposer = disposer;
    }
        
    public void Add(object obj)
    {
      if(obj is IInitializable initializable)
        _initializer.Add(initializable);
            
      if(obj is IFixedTickable fixedTickable)
        _ticker.AddFixed(fixedTickable);
            
      if(obj is ITickable tickable)
        _ticker.Add(tickable);
            
      if(obj is ILateTickable lateTickable)
        _ticker.AddLate(lateTickable);
            
      if(obj is IDisposable disposer)
        _disposer.Add(disposer);
            
      if(obj is ILateDisposable lateDisposable)
        _disposer.AddLate(lateDisposable);
    }

    public void Remove(object obj)
    {
      if(obj is IFixedTickable fixedTickable)
        _ticker.RemoveFixed(fixedTickable);
            
      if(obj is ITickable tickable)
        _ticker.Remove(tickable);
            
      if(obj is ILateTickable lateTickable)
        _ticker.RemoveLate(lateTickable);
            
      if(obj is IDisposable disposer)
        _disposer.Remove(disposer);
    }
  }
}
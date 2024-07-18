using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class InitializingPhase : IInitializingPhase, IInitializable
  {
    private readonly InitializableManager _initializer;
    public bool WasInitialized { get; private set; }

    public InitializingPhase(InitializableManager initializer)
    {
      _initializer = initializer;
      _initializer.Add(this, 9999);
    }
      
    public void Initialize()
    {
      WasInitialized = true;
    }

    public bool Add(IInitializable initializable)
    {
      if (!WasInitialized)
        _initializer.Add(initializable);

      return !WasInitialized;
    }

    public void EnsureInitializing(IInitializable initializable)
    {
      if(WasInitialized)
        initializable.Initialize();
      else
        _initializer.Add(initializable);
    }
  }
}
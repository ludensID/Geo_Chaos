using Zenject;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IInitializingPhase
  {
    bool WasInitialized { get; }
    void EnsureInitializing(IInitializable initializable);
  }
}
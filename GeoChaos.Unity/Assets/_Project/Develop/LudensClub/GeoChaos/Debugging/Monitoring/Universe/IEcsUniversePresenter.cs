using System.Collections.Generic;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsUniversePresenter
  {
    EcsUniverseView View { get; }
    List<IEcsWorldPresenter> Children { get; }
    bool WasInitialized { get; }
  }
}
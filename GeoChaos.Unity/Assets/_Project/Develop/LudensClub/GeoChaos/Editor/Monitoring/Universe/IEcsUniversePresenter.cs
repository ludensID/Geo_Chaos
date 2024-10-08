using System.Collections.Generic;
using LudensClub.GeoChaos.Editor.Monitoring.World;

namespace LudensClub.GeoChaos.Editor.Monitoring.Universe
{
  public interface IEcsUniversePresenter
  {
    EcsUniverseView View { get; }
    List<IEcsWorldPresenter> Children { get; }
    bool WasInitialized { get; }
  }
}
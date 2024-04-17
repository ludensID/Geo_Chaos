using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsWorldPresenterFactory
  {
    IEcsWorldPresenter Create(IEcsWorldWrapper world, IEcsUniversePresenter parent);
  }
}
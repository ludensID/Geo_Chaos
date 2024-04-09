using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsWorldPresenterFactory
  {
    IEcsWorldPresenter Create(IEcsWorldWrapper world, IEcsUniversePresenter parent);
  }
}
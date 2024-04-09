using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsEntityPresenterFactory
  {
    IEcsEntityPresenter Create(int entity, IEcsWorldPresenter parent, IEcsWorldWrapper wrapper);
  }
}
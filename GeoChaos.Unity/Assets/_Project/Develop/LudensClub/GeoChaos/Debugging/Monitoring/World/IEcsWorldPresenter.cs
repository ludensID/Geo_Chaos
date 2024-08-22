using System.Collections.Generic;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Debugging.Monitoring
{
  public interface IEcsWorldPresenter
  {
    EcsWorldView View { get; }
    List<IEcsEntityPresenter> Children { get; }
    IEcsWorldWrapper Wrapper { get; }
    void Initialize();
    void Tick();
  }
}
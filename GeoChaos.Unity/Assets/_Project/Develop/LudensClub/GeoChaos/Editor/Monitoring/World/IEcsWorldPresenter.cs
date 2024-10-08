using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Editor.Monitoring.Entity;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Editor.Monitoring.World
{
  public interface IEcsWorldPresenter
  {
    EcsWorldView View { get; }
    List<IEcsEntityPresenter> Children { get; }
    IEcsWorldWrapper Wrapper { get; }
    IEcsPool[] Pools { get; }
    void Initialize();
    void Tick();
  }
}
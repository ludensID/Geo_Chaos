using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsDisposer : IDisposable
  {
    List<IEcsWorldWrapper> Wrappers { get; }
    List<EcsSystems> Systems { get; }
  }
}
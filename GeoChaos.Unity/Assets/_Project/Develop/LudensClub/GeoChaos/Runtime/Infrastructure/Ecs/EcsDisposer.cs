using System.Collections.Generic;
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public class EcsDisposer : IEcsDisposer
  {
    public List<IEcsWorldWrapper> Wrappers { get; } = new();
    public List<EcsSystems> Systems { get; } = new();

    public EcsDisposer(List<IEcsWorldWrapper> wrappers)
    {
      Wrappers.AddRange(wrappers);
    }

    public void Dispose()
    {
      foreach (EcsSystems systems in Systems)
        systems.Destroy();

      foreach (IEcsWorldWrapper wrapper in Wrappers)
      {
        wrapper.World.Destroy();
      }
    }
  }
}
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsWorldWrapper
  {
    string Name { get; }
    EcsWorld World { get; }
  }
}
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsSystemFactory
  {
    IEcsSystem Create<TSystem>() where TSystem : IEcsSystem;
  }
}
using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsWorldProvider
  {
    EcsWorld World { get; set; }
  }
}
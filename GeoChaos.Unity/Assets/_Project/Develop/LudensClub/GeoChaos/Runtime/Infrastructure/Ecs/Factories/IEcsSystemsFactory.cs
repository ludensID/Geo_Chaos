using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsSystemsFactory
  {
    EcsSystems Create(string defaultName = EcsConstants.Worlds.GAME);
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;

namespace LudensClub.GeoChaos.Runtime.Infrastructure
{
  public interface IEcsSystemsFactory
  {
    EcsSystems Create(string defaultName = EcsConstants.Worlds.GAME);
  }
}
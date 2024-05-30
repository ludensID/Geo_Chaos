using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Constants;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class MessageWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world = new EcsWorld();

    public string Name => EcsConstants.Worlds.MESSAGE;
    public EcsWorld World => _world;
  }
}
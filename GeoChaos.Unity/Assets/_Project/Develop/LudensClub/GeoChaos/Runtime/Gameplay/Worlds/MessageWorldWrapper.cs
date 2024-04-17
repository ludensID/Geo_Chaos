using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class MessageWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world = new();

    public string Name => EcsConstants.Worlds.MESSAGE;
    public EcsWorld World => _world;
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class InputWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world = new();

    public string Name => EcsConstants.Worlds.INPUT;
    public EcsWorld World => _world;
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class GameWorldWrapper : IWorldWrapper
  {
    private EcsWorld _world = new EcsWorld();

    public string Name => "Game";
    public EcsWorld World => _world;

    public void Dispose()
    {
      if (_world != null)
      {
        _world.Destroy();
        _world = null;
      }
    }
  }
}
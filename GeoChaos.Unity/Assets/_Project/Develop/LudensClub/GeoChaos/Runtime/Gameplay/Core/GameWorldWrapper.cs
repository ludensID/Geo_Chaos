using Leopotam.EcsLite;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class GameWorldWrapper : IWorldWrapper
  {
    private EcsWorld _world = new EcsWorld();

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
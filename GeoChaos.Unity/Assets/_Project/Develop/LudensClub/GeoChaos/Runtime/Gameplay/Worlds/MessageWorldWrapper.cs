using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class MessageWorldWrapper : IEcsWorldWrapper
  {
    private EcsWorld _world = new();

    public string Name => EcsConstants.Worlds.MESSAGE;
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
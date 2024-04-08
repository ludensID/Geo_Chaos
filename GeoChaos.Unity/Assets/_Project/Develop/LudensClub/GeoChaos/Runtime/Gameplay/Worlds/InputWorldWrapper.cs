using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class InputWorldWrapper : IWorldWrapper
  {
    private EcsWorld _world = new();

    public string Name => "Input";
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
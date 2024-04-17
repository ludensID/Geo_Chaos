using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Worlds
{
  public class GameWorldWrapper : IEcsWorldWrapper
  {
    private readonly EcsWorld _world = new();

    public string Name => EcsConstants.Worlds.GAME;
    public EcsWorld World => _world;
  }
}
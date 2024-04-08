using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Creation.Systems
{
  public class DeleteOnConvertedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _converteds;

    public DeleteOnConvertedSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _converteds = _game
        .Filter<OnConverted>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var converted in _converteds) _game.Del<OnConverted>(converted);
    }
  }
}
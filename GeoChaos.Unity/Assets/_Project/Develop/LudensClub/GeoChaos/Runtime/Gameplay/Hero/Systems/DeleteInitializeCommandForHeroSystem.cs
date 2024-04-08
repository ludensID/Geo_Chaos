using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation.Components;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class DeleteInitializeCommandForHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;

    public DeleteInitializeCommandForHeroSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<Hero>()
        .Inc<InitializeCommand>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes) _game.Del<InitializeCommand>(hero);
    }
  }
}
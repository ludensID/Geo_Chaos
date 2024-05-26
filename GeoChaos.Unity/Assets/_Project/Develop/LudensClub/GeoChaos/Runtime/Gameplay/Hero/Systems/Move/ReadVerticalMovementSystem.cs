using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Move
{
  public class ReadVerticalMovementSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadVerticalMovementSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewDirection>()
        .Collect();

      _inputs = _input
        .Filter<VerticalMovement>()
        .Exc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity input in _inputs)
      foreach (EcsEntity hero in _heroes)
      {
        hero.Replace((ref ViewDirection viewDir) => viewDir.Direction.y = input.Get<VerticalMovement>().Direction);
      }
    }
  }
}
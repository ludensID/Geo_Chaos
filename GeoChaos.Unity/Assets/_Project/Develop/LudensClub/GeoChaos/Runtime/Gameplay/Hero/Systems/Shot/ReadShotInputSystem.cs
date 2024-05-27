using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shot;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shot
{
  public class ReadShotInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsWorld _input;
    private readonly EcsEntities _shootables;
    private readonly EcsEntities _inputs;

    public ReadShotInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _game = gameWorldWrapper.World;
      _input = inputWorldWrapper.World;

      _shootables = _game
        .Filter<ShootAvailable>()
        .Collect();

      _inputs = _input
        .Filter<IsShoot>()
        .Inc<Expired>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity shoot in _shootables)
      {
        shoot.Add<ShootCommand>();
      }
    }
  }
}
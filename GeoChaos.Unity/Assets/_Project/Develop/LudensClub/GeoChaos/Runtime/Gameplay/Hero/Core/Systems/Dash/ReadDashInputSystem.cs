using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class ReadDashInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _heroes;
    private readonly EcsEntities _inputs;

    public ReadDashInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _heroes = _world
        .Filter<DashAvailable>()
        .Exc<MovementLocked>()
        .Collect();

      _inputs = inputWorld
        .Filter<Expired>()
        .Inc<IsDash>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity _ in _inputs)
      foreach (EcsEntity hero in _heroes)
        hero.Add<DashCommand>();
    }
  }
}
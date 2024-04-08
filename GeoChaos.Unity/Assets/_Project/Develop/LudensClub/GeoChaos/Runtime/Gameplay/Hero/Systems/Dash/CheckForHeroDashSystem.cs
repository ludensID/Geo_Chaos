using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class CheckForHeroDashSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public CheckForHeroDashSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<Hero>()
        .Inc<DashAvailable>()
        .Inc<DashCommand>()
        .End();
    }


    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
        if (_world.Has<IsDashing>(hero))
          _world.Del<DashCommand>(hero);
    }
  }
}
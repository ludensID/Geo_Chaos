using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.Lock;
using LudensClub.GeoChaos.Runtime.Gameplay.Input;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class ReadDashInputSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly EcsFilter _inputs;

    public ReadDashInputSystem(GameWorldWrapper gameWorldWrapper, InputWorldWrapper inputWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      EcsWorld inputWorld = inputWorldWrapper.World;

      _heroes = _world
        .Filter<DashAvailable>()
        .Exc<IsMovementLocked>()
        .End();

      _inputs = inputWorld
        .Filter<Expired>()
        .Inc<IsDash>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var _ in _inputs)
      foreach (var hero in _heroes)
        _world.Add<DashCommand>(hero);
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class CheckStopHeroDashSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public CheckStopHeroDashSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world
        .Filter<DashAvailable>()
        .Inc<IsDashing>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref IsDashing isDashing = ref _world.Get<IsDashing>(hero);
        if (isDashing.TimeLeft <= 0)
          _world.Add<StopDashCommand>(hero);
      }
    }
  }
}
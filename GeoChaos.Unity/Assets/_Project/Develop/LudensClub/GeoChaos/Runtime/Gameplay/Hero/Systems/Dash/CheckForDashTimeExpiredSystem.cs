using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core.Dash
{
  public class CheckForDashTimeExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsEntities _dashes;

    public CheckForDashTimeExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _dashes = _world
        .Filter<DashAvailable>()
        .Inc<Dashing>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity dash in _dashes
        .Where<Dashing>(x => x.TimeLeft <= 0))
      {
        dash.Add<StopDashCommand>();
      }
    }
  }
}
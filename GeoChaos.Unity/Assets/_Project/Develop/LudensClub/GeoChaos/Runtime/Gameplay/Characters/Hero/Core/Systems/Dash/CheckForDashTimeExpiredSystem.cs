using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Dash;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero.Systems.Dash
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
        .Check<Dashing>(x => x.TimeLeft <= 0))
      {
        dash.Add<StopDashCommand>();
      }
    }
  }
}
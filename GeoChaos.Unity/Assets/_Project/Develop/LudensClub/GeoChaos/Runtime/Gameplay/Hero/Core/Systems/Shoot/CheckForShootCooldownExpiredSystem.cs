using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Hero.Shoot;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Systems.Shoot
{
  public class CheckForShootCooldownExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _timers;

    public CheckForShootCooldownExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _timers = _game
        .Filter<ShootCooldown>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _timers
        .Where<ShootCooldown>(x =>x.TimeLeft <= 0))
      {
        timer.Del<ShootCooldown>();
      }
    }
  }
}
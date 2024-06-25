using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class CheckLamaForSneakingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _expiredTimers;

    public CheckLamaForSneakingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _expiredTimers = _game
        .Filter<LamaTag>()
        .Inc<SneakingTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _expiredTimers
        .Where<SneakingTimer>(x => x.TimeLeft <= 0))
      {
        timer.Del<SneakingTimer>();
      }
    }
  }
}
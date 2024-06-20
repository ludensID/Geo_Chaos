using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class CheckLamaForLookingTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lookingTimers;

    public CheckLamaForLookingTimerExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lookingTimers = _game
        .Filter<LookingTimer>()
        .Inc<LamaTag>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity timer in _lookingTimers
        .Where<LookingTimer>(x => x.TimeLeft <= 0))
      {
        timer.Del<LookingTimer>();
      }
    }
  }
}
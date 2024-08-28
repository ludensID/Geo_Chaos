using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.ArmsAttack;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class StopZombieWatchWhenTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public StopZombieWatchWhenTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop(x => x.Exc<SpeedForceCommand>());

      _watchingZombies = _game
        .Filter<ZombieTag>()
        .Inc<WatchingTimer>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _watchingZombies
        .Check<WatchingTimer>(x => x.TimeLeft <= 0))
      {
        zombie
            .Del<WatchingTimer>()
            .Has<StopAttackWithArmsCycleCommand>(true);
        
        _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
      } 
    }
  }
}
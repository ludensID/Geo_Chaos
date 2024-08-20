using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class StopZombiePatrollingSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntities _patrollingZombies;

    public StopZombiePatrollingSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<StopPatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies)
      {
        if (zombie.Has<Patrolling>())
        {
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
          zombie.Del<Patrolling>();
        }

        zombie.Del<StopPatrolCommand>();
      }
    }
  }
}
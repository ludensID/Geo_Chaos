using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class CheckForZombieReachedMovePointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly ZombieConfig _config;
    private readonly EcsEntities _patrollingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForZombieReachedMovePointSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<ZombieConfig>();
      _forceLoop = forceLoopSvc.CreateLoop();

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<Patrolling>()
        .Exc<FinishPatrolCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies)
      {
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        float movePoint = zombie.Get<MovePoint>().Point;

        if (Mathf.Abs(movePoint - currentPoint) < _config.CalmSpeed * Time.fixedDeltaTime)
        {
          zombie.Add<FinishPatrolCommand>();
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
        }
      }
    }
  }
}
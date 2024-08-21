using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Patrol
{
  public class ZombieReachingPatrolPointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public ZombieReachingPatrolPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
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
        float speed = zombie.Get<MovementVector>().Speed.x;

        if (Mathf.Abs(movePoint - currentPoint) < speed * Time.fixedDeltaTime)
        {
          zombie.Add<FinishPatrolCommand>();
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
        }
      }
    }
  }
}
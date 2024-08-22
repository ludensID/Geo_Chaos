using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Watch;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Watch
{
  public class ZombieReachingWatchPointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _watchingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public ZombieReachingWatchPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
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
      foreach (EcsEntity zombie in _watchingZombies)
      {
        float currentPoint = zombie.Get<ViewRef>().View.transform.position.x;
        float movePoint = zombie.Get<MovePoint>().Point;
        float speed = zombie.Get<MovementVector>().Speed.x;

        if (Mathf.Abs(movePoint - currentPoint) < speed * Time.fixedDeltaTime)
        {
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
        }
      }
    }
  }
}
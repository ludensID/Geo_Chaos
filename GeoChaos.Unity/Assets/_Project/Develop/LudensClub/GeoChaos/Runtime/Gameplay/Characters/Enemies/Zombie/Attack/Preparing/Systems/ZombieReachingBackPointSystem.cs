using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.Preparing
{
  public class ZombieReachingBackPointSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public ZombieReachingBackPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _patrollingZombies = _game
        .Filter<ZombieTag>()
        .Inc<AttackPreparingTimer>()
        .Exc<FinishPrepareToAttackCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _patrollingZombies
        .Check<AttackPreparingTimer>(x => x.TimeLeft <= 0))
      {
          zombie.Add<FinishPrepareToAttackCommand>();
          _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
      }
    }
  }
}
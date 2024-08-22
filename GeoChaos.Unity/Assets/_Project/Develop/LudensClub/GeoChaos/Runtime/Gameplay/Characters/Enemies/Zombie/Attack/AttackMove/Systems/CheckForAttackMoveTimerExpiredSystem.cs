using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class CheckForAttackMoveTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingZombies;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForAttackMoveTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movingZombies = _game
        .Filter<ZombieTag>()
        .Inc<AttackMoveTimer>()
        .Exc<FinishAttackMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity zombie in _movingZombies
        .Check<AttackMoveTimer>(x => x.TimeLeft <= 0))
      {
        _forceLoop.ResetForcesToZero(SpeedForceType.Move, zombie.PackedEntity);
        zombie
          .Del<AttackMoveTimer>()
          .Add<FinishAttackMoveCommand>();
      }
    }
  }
}
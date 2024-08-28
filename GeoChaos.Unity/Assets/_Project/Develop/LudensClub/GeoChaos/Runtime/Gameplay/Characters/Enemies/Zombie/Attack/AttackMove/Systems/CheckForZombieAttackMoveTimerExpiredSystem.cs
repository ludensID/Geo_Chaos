using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie.Attack.AttackMove
{
  public class CheckForZombieAttackMoveTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingEntities;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForZombieAttackMoveTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movingEntities = _game
        .Filter<ZombieTag>()
        .Inc<AttackMoveTimer>()
        .Exc<FinishAttackMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _movingEntities
        .Check<AttackMoveTimer>(x => x.TimeLeft <= 0))
      {
        _forceLoop.ResetForcesToZero(SpeedForceType.Attack, entity.PackedEntity);
        entity
          .Del<AttackMoveTimer>()
          .Add<FinishAttackMoveCommand>();
      }
    }
  }
}
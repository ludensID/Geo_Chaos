using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class CheckForAttackMoveTimerExpiredSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingEntities;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForAttackMoveTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _movingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<AttackMoveTimer>()
        .Exc<FinishAttackMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _movingEntities
        .Check<AttackMoveTimer>(x => x.TimeLeft <= 0))
      {
        _forceLoop.ResetForcesToZero(SpeedForceType.Move, entity.PackedEntity);
        entity
          .Del<AttackMoveTimer>()
          .Add<FinishAttackMoveCommand>();
      }
    }
  }
}
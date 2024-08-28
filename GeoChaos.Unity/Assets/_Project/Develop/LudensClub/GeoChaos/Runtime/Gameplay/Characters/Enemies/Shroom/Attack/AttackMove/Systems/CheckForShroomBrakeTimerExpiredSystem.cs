using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Damage;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Shroom.Attack.AttackMove
{
  public class CheckForShroomBrakeTimerExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _brakingShrooms;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForShroomBrakeTimerExpiredSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _brakingShrooms = _game
        .Filter<ShroomTag>()
        .Inc<BrakeTimer>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity shroom in _brakingShrooms
        .Check<BrakeTimer>(x => x.TimeLeft <= 0))
      {
        _forceLoop.ResetForcesToZero(SpeedForceType.Attack, shroom.PackedEntity);
        shroom
          .Del<BrakeTimer>()
          .Add<FinishAttackMoveCommand>()
          .Add<FinishAttackCommand>();
      }
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Attack.AttackMove
{
  public class FinishAttackMovingSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingEntities;

    public FinishAttackMovingSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _movingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<FinishAttackMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _movingEntities)
      {
        entity
          .Del<FinishAttackMoveCommand>()
          .Del<AttackMoving>()
          .Add<OnAttackMovingFinished>();
      }
    }
  }
}
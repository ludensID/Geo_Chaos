using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class FinishPatrolSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingEntities;

    public FinishPatrolSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _patrollingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<FinishPatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _patrollingEntities)
      {
        entity
          .Del<FinishPatrolCommand>()
          .Del<Patrolling>()
          .Add<OnPatrolFinished>();
      }
    }
  }
}
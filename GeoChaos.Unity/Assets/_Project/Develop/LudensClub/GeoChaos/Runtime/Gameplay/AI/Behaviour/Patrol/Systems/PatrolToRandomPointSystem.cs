using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Zombie;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol
{
  public class PatrolToRandomPointSystem<TFilterComponent> : IEcsRunSystem
    where TFilterComponent : struct, IEcsComponent
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _patrollingEntities;

    public PatrolToRandomPointSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _patrollingEntities = _game
        .Filter<TFilterComponent>()
        .Inc<PatrolCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _patrollingEntities)
      {
        float currentPoint = entity.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = entity.Get<PatrolBounds>().HorizontalBounds;
        float nextPoint = Random.Range(bounds.x, bounds.y);
        float direction = Mathf.Sign(nextPoint - currentPoint);
        
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, entity.PackedEntity, Vector2.right)
        {
          Speed = Vector2.right * entity.Get<PatrolSpeed>().Speed,
          Direction = Vector2.right * direction
        });

        entity
          .Del<PatrolCommand>()
          .Add<Patrolling>()
          .Replace((ref MovePoint point) => point.Point = nextPoint)
          .Change((ref BodyDirection bodyDirection) =>
            bodyDirection.Direction = direction);
      }
    }
  }
}
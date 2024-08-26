using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.AI.Patrol
{
  public class CheckForEntitiesInBoundsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _entities;

    public CheckForEntitiesInBoundsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _entities = _game
        .Filter<PatrolBounds>()
        .Inc<ViewRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity entity in _entities)
      {
        float point = entity.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = entity.Get<PatrolBounds>().HorizontalBounds;
        entity.Has<InBounds>(bounds.x < point && point < bounds.y);
      }
    }
  }
}
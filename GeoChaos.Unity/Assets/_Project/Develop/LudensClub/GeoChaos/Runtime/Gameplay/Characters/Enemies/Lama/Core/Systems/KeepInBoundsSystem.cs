using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.Lama
{
  public class KeepInBoundsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _boundeds;

    public KeepInBoundsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _boundeds = _game
        .Filter<MovementVector>()
        .Inc<PatrolBounds>()
        .Inc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity bounded in _boundeds)
      {
        Vector2 bounds = bounded.Get<PatrolBounds>().Bounds;
        Rigidbody2D rb = bounded.Get<RigidbodyRef>().Rigidbody;
        float direction = bounded.Get<MovementVector>().Direction.x;
        int index = GetBoundIndex(rb.position.x, direction, bounds);
        if (index >= 0)
        {
          Vector3 position = rb.position;
          position.x = bounds[index];
          rb.MovePosition(position);

          bounded.Change((ref MovementVector vector) => vector.Speed.x = 0);
        }
      }
    }

    private int GetBoundIndex(float positionX, float direction, Vector2 bounds)
    {
      if (positionX < bounds.x && direction < 0)
        return 0;
      if (positionX > bounds.y && direction > 0)
        return 1;
      return -1;
    }
  }
}
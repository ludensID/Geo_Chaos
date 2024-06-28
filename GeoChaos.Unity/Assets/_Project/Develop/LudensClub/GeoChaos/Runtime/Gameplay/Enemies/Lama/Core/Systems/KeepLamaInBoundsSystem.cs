using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.View;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class KeepLamaInBoundsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _lamas;

    public KeepLamaInBoundsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _lamas = _game
        .Filter<LamaTag>()
        .Inc<ViewRef>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _lamas)
      {
        Vector2 bounds = lama.Get<PatrolBounds>().Bounds;
        Rigidbody2D rb = lama.Get<RigidbodyRef>().Rigidbody;
        float direction = lama.Get<MovementVector>().Direction.x;
        int index = GetBoundIndex(rb.position.x, direction, bounds);
        if (index >= 0)
        {
          Vector3 position = rb.position;
          position.x = bounds[index];
          rb.MovePosition(position);

          lama.Replace((ref MovementVector vector) => vector.Speed.x = 0);
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
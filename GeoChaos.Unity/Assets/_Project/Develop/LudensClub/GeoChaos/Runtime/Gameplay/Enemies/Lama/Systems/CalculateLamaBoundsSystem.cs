using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.Lama
{
  public class CalculateLamaBoundsSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _initializedLamas;

    public CalculateLamaBoundsSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _initializedLamas = _game
        .Filter<LamaTag>()
        .Inc<CalculateBoundsCommand>()
        .Inc<StartTransform>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity lama in _initializedLamas)
      {
        var ctx = lama.Get<BrainContext>().Cast<LamaContext>();
        Vector3 origin = lama.Get<StartTransform>().Position;
        Vector2 physicalBounds = lama.Get<PhysicalBounds>().Bounds;

        lama
          .Replace((ref PatrolBounds patrolBounds) =>
            patrolBounds.Bounds = ClampBounds(origin, ctx.PatrolAreaLength / 2, physicalBounds))
          .Replace((ref ChasingBounds bounds) =>
            bounds.Bounds = ClampBounds(origin, ctx.PatrolAreaLength * 1.5f, physicalBounds));

        lama.Del<CalculateBoundsCommand>();
      }
    }

    private static Vector2 ClampBounds(Vector2 origin, float radius, Vector2 physicalBounds)
    {
      var bounds = new Vector2(origin.x - radius, origin.x + radius);
      bounds.x = MathUtils.Clamp(bounds.x, physicalBounds.x);
      bounds.y = MathUtils.Clamp(bounds.y, max: physicalBounds.y);
      return bounds;
    }
  }
}
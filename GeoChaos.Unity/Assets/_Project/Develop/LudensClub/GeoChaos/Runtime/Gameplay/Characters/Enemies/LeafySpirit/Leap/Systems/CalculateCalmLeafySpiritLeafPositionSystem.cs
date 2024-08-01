using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class CalculateCalmLeafySpiritLeafPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;
    private readonly LeafySpiritConfig _config;

    public CalculateCalmLeafySpiritLeafPositionSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<LeapPoint>()
        .Exc<Aimed>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits)
      {
        float point = spirit.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = spirit.Get<PatrolBounds>().Bounds;
        spirit.Change((ref LeapPoint leapPoint) => leapPoint.Point = CalculateNextPosition(point, bounds));
      }
    }

    private float CalculateNextPosition(float point, Vector2 bounds)
    {
      float direction = Mathf.Sign(Random.Range(-1, 1));

      float center = (bounds.y - bounds.x) / 2;
      float centerPoint = bounds.x + center;
      float distance = Random.Range(_config.MinLeapDistance, center);

      float nextPoint = point + direction * distance;
      if (nextPoint < bounds.x || nextPoint > bounds.y)
      {
        nextPoint = point.ApproximatelyEqual(centerPoint)
          ? MathUtils.Clamp(nextPoint, bounds.x, bounds.y)
          : centerPoint;
      }

      return nextPoint;
    }
  }
}
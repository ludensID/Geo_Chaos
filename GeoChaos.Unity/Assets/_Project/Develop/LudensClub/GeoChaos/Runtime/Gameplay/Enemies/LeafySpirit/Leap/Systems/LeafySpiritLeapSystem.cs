using System;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Enemies.LeafySpirit.Leap
{
  public class LeafySpiritLeapSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;
    private readonly LeafySpiritConfig _config;

    public LeafySpiritLeapSystem(GameWorldWrapper gameWorldWrapper,
      ITimerFactory timers,
      IConfigProvider configProvider)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<PrecastLeapTimer>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits
        .Where<PrecastLeapTimer>(x => x.TimeLeft <= 0))
      {
        spirit.Del<PrecastLeapTimer>();

        Transform transform = spirit.Get<ViewRef>().View.transform;
        ref PatrolBounds bounds = ref spirit.Get<PatrolBounds>();
        transform.position = CalculateNextPosition(transform.position, bounds.Bounds);

        spirit.Add((ref LeapTimer timer) => timer.TimeLeft = _timers.Create(_config.LeapTime));
      }
    }

    private Vector3 CalculateNextPosition(Vector3 position, Vector2 bounds)
    {
      float direction = Mathf.Sign(Random.Range(-1, 1));

      float center = (bounds.y - bounds.x) / 2;
      float distance = Random.Range(_config.MinLeapDistance, center);

      float nextPosition = position.x + direction * distance;
      if (nextPosition < bounds.x || nextPosition > bounds.y)
      {
        nextPosition = position.x.ApproximatelyEqual(center)
          ? MathUtils.Clamp(nextPosition, bounds.x, bounds.y)
          : center;
      }

      position.x = nextPosition;
      return position;
    }
  }
}
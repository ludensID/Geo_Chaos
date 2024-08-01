using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
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
        .Inc<LeapPoint>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _leapingSpirits)
      {
        Transform transform = spirit.Get<ViewRef>().View.transform;
        Vector3 nextPosition = transform.position;
        nextPosition.x = spirit.Get<LeapPoint>().Point;
        transform.position = nextPosition;

        spirit
          .Del<LeapPoint>()
          .Add((ref LeapTimer timer) => timer.TimeLeft = _timers.Create(_config.LeapTime));
      }
    }
  }
}
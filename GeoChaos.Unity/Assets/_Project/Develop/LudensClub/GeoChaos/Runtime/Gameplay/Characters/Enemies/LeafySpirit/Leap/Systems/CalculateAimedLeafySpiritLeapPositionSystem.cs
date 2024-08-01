using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Leap
{
  public class CalculateAimedLeafySpiritLeapPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _leapingSpirits;
    private readonly EcsEntities _heroes;
    private readonly LeafySpiritConfig _config;

    public CalculateAimedLeafySpiritLeapPositionSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Collect();

      _leapingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<LeapPoint>()
        .Inc<Aimed>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity spirit in _leapingSpirits)
      {
        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = spirit.Get<PatrolBounds>().Bounds;
        spirit.Change((ref LeapPoint point) => point.Point = CalculateNextPoint(heroPoint, bounds));
      }
    }

    private float CalculateNextPoint(float point, Vector2 bounds)
    {
      float distance = Random.Range(0f, _config.AttackDistance);

      int left = -1;
      int right = 1;

      if (point + distance * left < bounds.x)
        left = 0;
      
      if (point + distance * right > bounds.y)
        right = 0;

      if (right == 0 && left == 0)
        return MathUtils.Clamp(point + distance * Mathf.Sign(Random.Range(-1, 1)), bounds.x, bounds.y);
      
      float direction = Mathf.Sign(Random.Range(left, right));
      return point + distance * direction;
    }
  }
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.AI.Behaviour.Patrol;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class CheckForLeafySpiritReachPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingSpirits;
    private readonly EcsEntities _heroes;
    private readonly LeafySpiritConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForLeafySpiritReachPositionSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafySpiritConfig>();
      _forceLoop = forceLoopSvc.CreateLoop();

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();

      _movingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<Moving>()
        .Exc<FinishMoveCommand>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      foreach (EcsEntity spirit in _movingSpirits)
      {
        float heroPoint = hero.Get<ViewRef>().View.transform.position.x;
        float spiritPoint = spirit.Get<ViewRef>().View.transform.position.x;
        Vector2 bounds = spirit.Get<PatrolBounds>().HorizontalBounds;

        float distance = Mathf.Abs(heroPoint - spiritPoint);
        if (distance < _config.AttackDistance * _config.AttackDistanceMultiplier
          || spiritPoint.ApproximatelyEqual(bounds.x) || spiritPoint.ApproximatelyEqual(bounds.y))
        {
          spirit.Add<FinishMoveCommand>();

          foreach (EcsEntity force in _forceLoop
            .GetLoop(SpeedForceType.Move, spirit.PackedEntity))
          {
            force
              .Has<Instant>(true)
              .Change((ref MovementVector vector) => vector.Speed = Vector2.zero);
          }
        }
      }
    }
  }
}
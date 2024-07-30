using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
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
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 spiritPosition = spirit.Get<ViewRef>().View.transform.position;

        float distance = Mathf.Abs(heroPosition.x - spiritPosition.x);
        if (distance < _config.AttackDistance * 0.8f)
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
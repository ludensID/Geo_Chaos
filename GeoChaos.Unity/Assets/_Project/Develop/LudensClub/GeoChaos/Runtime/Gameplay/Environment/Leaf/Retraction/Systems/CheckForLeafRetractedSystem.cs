using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class CheckForLeafRetractedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractingLeaves;
    private readonly LeafConfig _config;
    private readonly SpeedForceLoop _forceLoop;
    private readonly EcsEntity _cachedSpirit;

    public CheckForLeafRetractedSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafConfig>();
      _forceLoop = forceLoopSvc.CreateLoop();

      _retractingLeaves = _game
        .Filter<LeafTag>()
        .Inc<Retracting>()
        .Inc<Owner>()
        .Collect();

      _cachedSpirit = new EcsEntity(_game);
    }
      
    public void Run(EcsSystems systems)
    {

      foreach (EcsEntity leaf in _retractingLeaves)
      {
        if (leaf.Get<Owner>().Entity.TryUnpackEntity(_game, _cachedSpirit))
        {
          Vector3 spiritPosition = _cachedSpirit.Get<ViewRef>().View.transform.position;
          Vector3 leafPosition = leaf.Get<ViewRef>().View.transform.position;
          float distance = ((Vector2)(spiritPosition - leafPosition)).magnitude;

          if (distance <= _config.RetractionSpeed * Time.fixedDeltaTime)
          {
            foreach (EcsEntity force in _forceLoop.GetLoop(SpeedForceType.Move, leaf.PackedEntity))
            {
              force
                .Change((ref MovementVector vector) => vector.Speed = Vector2.zero)
                .Add<Instant>();
            }

            leaf
              .Del<Retracting>()
              .Add<DestroyCommand>();
          }
        }
      }
    }
  }
}
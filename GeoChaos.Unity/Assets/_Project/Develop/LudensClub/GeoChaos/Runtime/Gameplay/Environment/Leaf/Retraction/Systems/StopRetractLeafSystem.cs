using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class StopRetractLeafSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractedLeaves;
    private readonly SpeedForceLoop _forceLoop;

    public StopRetractLeafSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _forceLoop = forceLoopSvc.CreateLoop();

      _retractedLeaves = _game
        .Filter<LeafTag>()
        .Inc<StopRetractCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _retractedLeaves)
      {
        leaf.Del<StopRetractCommand>();
        
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
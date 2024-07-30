using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class CheckForLeafReachedPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingLeaves;
    private readonly LeafConfig _config;
    private readonly SpeedForceLoop _forceLoop;

    public CheckForLeafReachedPositionSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceLoopService forceLoopSvc)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafConfig>();
      _forceLoop = forceLoopSvc.CreateLoop();

      _movingLeaves = _game
        .Filter<LeafTag>()
        .Inc<Moving>()
        .Inc<ViewRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _movingLeaves)
      {
        Vector3 start = leaf.Get<StartMovePosition>().Position;
        Vector3 position = leaf.Get<ViewRef>().View.transform.position;

        float distance = (position - start).magnitude;
        foreach (EcsEntity force in _forceLoop
          .GetLoop(SpeedForceType.Move, leaf.PackedEntity))
        {
          force.Change((ref MovementVector vector) =>
            vector.Speed.x = _config.SpeedCurve.Evaluate(distance / _config.Distance) * _config.Speed);
        }
          
        if (distance >= _config.Distance)
          leaf.Add<StopMoveCommand>();
      }
    }
  }
}
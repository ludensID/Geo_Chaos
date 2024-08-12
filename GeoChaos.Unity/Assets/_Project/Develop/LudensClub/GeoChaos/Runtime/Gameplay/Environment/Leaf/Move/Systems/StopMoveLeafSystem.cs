using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move
{
  public class StopMoveLeafSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingLeaves;

    public StopMoveLeafSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;

      _movingLeaves = _game
        .Filter<LeafTag>()
        .Inc<Moving>()
        .Inc<StopMoveCommand>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _movingLeaves)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, leaf.PackedEntity, Vector2.one));

        leaf
          .Del<StopMoveCommand>()
          .Del<Moving>();
      }
    }
  }
}
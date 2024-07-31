using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Move
{
  public class MoveLeafSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingLeaves;
    private readonly LeafConfig _config;

    public MoveLeafSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory, IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafConfig>();

      _movingLeaves = _game
        .Filter<LeafTag>()
        .Inc<MoveCommand>()
        .Inc<MoveDirection2>()
        .Inc<ViewRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _movingLeaves)
      {
        (Vector3 length, Vector3 direction) =
          MathUtils.DecomposeVector(leaf.Get<MoveDirection2>().Direction * _config.MoveSpeed);
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, leaf.PackedEntity, Vector2.one)
        {
          Speed = length,
          Direction = direction
        });

        leaf
          .Del<MoveCommand>()
          .Del<MoveDirection2>()
          .Add<Moving>()
          .Change((ref StartMovePosition position) => position.Position = leaf.Get<ViewRef>().View.transform.position);
      }
    }
  }
}
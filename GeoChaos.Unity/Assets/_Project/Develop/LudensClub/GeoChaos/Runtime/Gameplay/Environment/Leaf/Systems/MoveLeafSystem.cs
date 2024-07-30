using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
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
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _movingLeaves)
      {
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, leaf.PackedEntity, Vector2.one)
        {
          Speed = Vector2.one * _config.Speed,
          Direction = leaf.Get<MoveDirection2>().Direction
        });

        leaf
          .Del<MoveCommand>()
          .Del<MoveDirection2>()
          .Add<Moving>();
      }
    }
  }
}
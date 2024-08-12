using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Tongue.Move
{
  public class TongueMovingSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly TongueConfig _config;
    private readonly EcsEntities _movingTongues;

    public TongueMovingSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<TongueConfig>();

      _movingTongues = _game
        .Filter<TongueTag>()
        .Inc<MoveCommand>()
        .Inc<MovePosition>()
        .Inc<ViewRef>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity tongue in _movingTongues)
      {
        tongue.Del<MoveCommand>();
        
        Vector2 position = tongue.Get<ViewRef>().View.transform.position;
        Vector2 movePosition = tongue.Get<MovePosition>().Position;
        
        Vector2 vector = (movePosition - position).normalized * _config.Speed;
        (Vector3 length, Vector3 direction) = MathUtils.DecomposeVector(vector);
        
        _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, tongue.PackedEntity, Vector2.one)
        {
          Speed = length,
          Direction = direction
        });

        tongue.Add<Moving>();
      }
    }
  }
}
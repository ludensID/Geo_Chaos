using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Move;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Move
{
  public class StopMoveLeafySpiritSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingSpirits;

    public StopMoveLeafySpiritSystem(GameWorldWrapper gameWorldWrapper, ISpeedForceFactory forceFactory)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      
      _movingSpirits = _game
        .Filter<LeafySpiritTag>()
        .Inc<StopMoveCommand>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity spirit in _movingSpirits)
      {
        spirit.Del<StopMoveCommand>();

        if (spirit.Has<Moving>())
        {
          spirit.Del<Moving>();
          
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, spirit.PackedEntity, Vector2.right)
          {
            Instant = true
          });
        }
      }
    }
  }
}
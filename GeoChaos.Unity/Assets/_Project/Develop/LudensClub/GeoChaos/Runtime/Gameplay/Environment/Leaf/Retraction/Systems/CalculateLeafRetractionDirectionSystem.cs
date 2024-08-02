using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class CalculateLeafRetractionDirectionSystem : IEcsRunSystem
  {
    private readonly ISpeedForceFactory _forceFactory;
    private readonly EcsWorld _game;
    private readonly LeafConfig _config;
    private readonly EcsEntities _retractedLeaves;
    private readonly EcsEntity _cachedSpirit;

    public CalculateLeafRetractionDirectionSystem(GameWorldWrapper gameWorldWrapper,
      ISpeedForceFactory forceFactory,
      IConfigProvider configProvider)
    {
      _forceFactory = forceFactory;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafConfig>();

      _retractedLeaves = _game
        .Filter<LeafTag>()
        .Inc<Retracting>()
        .Inc<Owner>()
        .Collect();

      _cachedSpirit = new EcsEntity(_game);
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity leaf in _retractedLeaves)
      {
        if (leaf.Get<Owner>().Entity.TryUnpackEntity(_game, _cachedSpirit))
        {
          Vector3 spiritPosition = _cachedSpirit.Get<ViewRef>().View.transform.position;
          Vector3 leafPosition = leaf.Get<ViewRef>().View.transform.position;
          Vector2 direction = ((Vector2)(spiritPosition - leafPosition)).normalized;
          
          (Vector3 Length, Vector3 Direction) vector = MathUtils.DecomposeVector(direction * _config.RetractionSpeed);
          _forceFactory.Create(new SpeedForceData(SpeedForceType.Move, leaf.PackedEntity, Vector2.one)
          {
            Speed = vector.Length,
            Direction = vector.Direction
          });
        }
      }
    }
  }
}
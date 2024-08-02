using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Characters.Enemies.LeafySpirit.Retraction;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf.Retraction
{
  public class CheckForLeafRetractedSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _retractedLeaves;
    private readonly LeafConfig _config;
    private readonly EcsEntity _cachedSpirit;

    public CheckForLeafRetractedSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
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
          Vector2 spiritPosition = _cachedSpirit.Get<ViewRef>().View.transform.position;
          Vector2 leafPosition = leaf.Get<ViewRef>().View.transform.position;
          float distance = (spiritPosition - leafPosition).magnitude;
          if (distance <= _config.RetractionSpeed * Time.fixedDeltaTime)
          {
            leaf.Add<StopRetractCommand>();
          }
        }
      }
    }
  }
}
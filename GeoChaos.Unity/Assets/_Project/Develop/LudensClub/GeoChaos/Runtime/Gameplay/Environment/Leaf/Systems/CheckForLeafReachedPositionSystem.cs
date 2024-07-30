using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.Leaf
{
  public class CheckForLeafReachedPositionSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _movingLeaves;
    private readonly LeafConfig _config;

    public CheckForLeafReachedPositionSystem(GameWorldWrapper gameWorldWrapper, ConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<LeafConfig>();

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

        if ((position - start).sqrMagnitude >= Mathf.Pow(_config.Distance, 2))
          leaf.Add<StopMoveCommand>();
      }
    }
  }
}
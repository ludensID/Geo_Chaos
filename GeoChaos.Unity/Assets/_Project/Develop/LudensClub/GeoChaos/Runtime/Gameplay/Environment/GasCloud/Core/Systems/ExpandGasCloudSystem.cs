using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class ExpandGasCloudSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _expandingClouds;
    private readonly GasCloudConfig _config;

    public ExpandGasCloudSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<GasCloudConfig>();

      _expandingClouds = _game
        .Filter<GasCloudTag>()
        .Inc<CloudSize>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cloud in _expandingClouds)
      {
        cloud.Change((ref CloudSize cloudSize) => cloudSize.Size += _config.ExpandSpeed * Time.deltaTime);
      }
    }
  }
}
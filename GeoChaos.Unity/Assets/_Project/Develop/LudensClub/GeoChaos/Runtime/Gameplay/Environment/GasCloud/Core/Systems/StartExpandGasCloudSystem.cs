using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Creation;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class StartExpandGasCloudSystem : IEcsRunSystem
  {
    private readonly ITimerFactory _timers;
    private readonly EcsWorld _game;
    private readonly EcsEntities _clouds;
    private readonly GasCloudConfig _config;

    public StartExpandGasCloudSystem(GameWorldWrapper gameWorldWrapper,
      IConfigProvider configProvider,
      ITimerFactory timers)
    {
      _timers = timers;
      _game = gameWorldWrapper.World;
      _config = configProvider.Get<GasCloudConfig>();

      _clouds = _game
        .Filter<GasCloudTag>()
        .Inc<OnConverted>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cloud in _clouds)
      {
        cloud
          .Add((ref LifeTime lifeTime) => lifeTime.TimeLeft = _timers.Create(_config.LifeTime))
          .Add((ref CloudSize cloudSize) => cloudSize.Size = _config.MinSize);
      }
    }
  }
}
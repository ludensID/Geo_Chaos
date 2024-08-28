using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Core.Destroying;
using LudensClub.GeoChaos.Runtime.Infrastructure;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class CheckForGasCloudLifeTimeExpiredSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _clouds;

    public CheckForGasCloudLifeTimeExpiredSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _clouds = _game
        .Filter<GasCloudTag>()
        .Inc<LifeTime>()
        .Collect();
    }

    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cloud in _clouds
        .Check<LifeTime>(x => x.TimeLeft <= 0))
      {
        cloud.Add<DestroyCommand>();
      }
    }
  }
}
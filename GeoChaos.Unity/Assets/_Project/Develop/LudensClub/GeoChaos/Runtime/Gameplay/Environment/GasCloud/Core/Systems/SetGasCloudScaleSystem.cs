using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Environment.GasCloud
{
  public class SetGasCloudScaleSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _clouds;

    public SetGasCloudScaleSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _clouds = _game
        .Filter<GasCloudTag>()
        .Inc<ViewRef>()
        .Inc<CloudSize>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity cloud in _clouds)
      {
        float size = cloud.Get<CloudSize>().Size;
        Transform transform = cloud.Get<ViewRef>().View.transform;
        Vector3 scale = transform.localScale;
        scale = new Vector3(size, size, scale.x);
        transform.localScale = scale;
      }
    }
  }
}
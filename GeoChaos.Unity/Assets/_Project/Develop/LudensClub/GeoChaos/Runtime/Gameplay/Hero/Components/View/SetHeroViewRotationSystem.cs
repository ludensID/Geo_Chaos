using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View
{
  public class SetHeroViewRotationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _views;

    public SetHeroViewRotationSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _views = _game
        .Filter<ViewRef>()
        .Inc<ViewDirection>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity view in _views)
      {
        view.Replace((ref ViewRef viewRef) =>
        {
          Vector3 rotation = viewRef.View.transform.eulerAngles;
          rotation.y = view.Get<ViewDirection>().Direction >= 0 ? 0 : 180;
          viewRef.View.transform.eulerAngles = rotation;
        });
      }
    }
  }
}
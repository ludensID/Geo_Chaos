using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.View
{
  public class SetViewRotationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsEntities _views;

    public SetViewRotationSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _views = _game
        .Filter<ViewRef>()
        .Inc<BodyDirection>()
        .Collect();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity view in _views)
      {
        view.Change((ref ViewRef viewRef) =>
        {
          Vector3 rotation = viewRef.View.transform.eulerAngles;
          rotation.y = view.Get<BodyDirection>().Direction >= 0 ? 0 : 180;
          viewRef.View.transform.eulerAngles = rotation;
        });
      }
    }
  }
}
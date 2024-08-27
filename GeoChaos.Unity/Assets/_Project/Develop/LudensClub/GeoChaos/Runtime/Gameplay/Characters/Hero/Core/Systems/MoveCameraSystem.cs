using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Characters.Hero
{
  public class MoveCameraSystem : IEcsRunSystem
  {
    private readonly ICameraProvider _cameraProvider;
    private readonly EcsWorld _game;
    private readonly EcsEntities _heroes;

    public MoveCameraSystem(GameWorldWrapper gameWorldWrapper, ICameraProvider cameraProvider)
    {
      _cameraProvider = cameraProvider;
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Collect();
    }
      
    public void Run(EcsSystems systems)
    {
      foreach (EcsEntity hero in _heroes)
      {
        Vector3 heroPosition = hero.Get<ViewRef>().View.transform.position;
        Vector3 position = _cameraProvider.Camera.transform.position;
        position.x = heroPosition.x;
        position.y = heroPosition.y;
        _cameraProvider.Camera.transform.position = position;
      }
    }
  }
}
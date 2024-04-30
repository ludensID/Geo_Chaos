using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Core;
using LudensClub.GeoChaos.Runtime.Gameplay.Physics.Forces;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Infrastructure;
using LudensClub.GeoChaos.Runtime.Utils;
using UnityEngine;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Hero.Components.View
{
  public class SetHeroViewRotationSystem : IEcsRunSystem
  {
    private readonly EcsWorld _game;
    private readonly EcsFilter _heroes;

    public SetHeroViewRotationSystem(GameWorldWrapper gameWorldWrapper)
    {
      _game = gameWorldWrapper.World;

      _heroes = _game
        .Filter<HeroTag>()
        .Inc<ViewRef>()
        .Inc<MovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref MovementVector vector = ref _game.Get<MovementVector>(hero);
        ref ViewRef viewRef = ref _game.Get<ViewRef>(hero);
        Vector3 rotation = viewRef.View.transform.eulerAngles;
        rotation.y = vector.Direction.x >= 0 ? 0 : 180;
        viewRef.View.transform.eulerAngles = rotation;
      }
    }
  }
}
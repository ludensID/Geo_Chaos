using System.Collections.Generic;
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class InitializeHeroMovementSystem : IEcsInitSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public InitializeHeroMovementSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;
      _heroes = _world.Filter<Hero>().End();
    }

    public void Init(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref Movable movable = ref _world.Add<Movable>(hero);
        movable.CanMove = true;

        ref MovementQueue movementQueue = ref _world.Add<MovementQueue>(hero);
        movementQueue.NextMovements = new Queue<DelayedMovement>();

        _world.Add<HeroMovementVector>(hero);
        _world.Add<HeroVelocity>(hero);
        _world.Add<Ground>(hero);
        ref JumpAvailable jumpAvailable = ref _world.Add<JumpAvailable>(hero);
        jumpAvailable.IsJumping = true;
      }
    }
  }
}
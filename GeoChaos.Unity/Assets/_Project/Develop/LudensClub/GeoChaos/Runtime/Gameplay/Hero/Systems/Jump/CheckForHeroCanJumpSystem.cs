using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroCanJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;

    public CheckForHeroCanJumpSystem(GameWorldWrapper gameWorldWrapper)
    {
      _world = gameWorldWrapper.World;

      _heroes = _world.Filter<Hero>()
        .Inc<IsJumping>()
        .Inc<HeroMovementVector>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        if (vector.Direction.y <= 0)
          _world.Del<IsJumping>(hero);
      }
    }
  }
}
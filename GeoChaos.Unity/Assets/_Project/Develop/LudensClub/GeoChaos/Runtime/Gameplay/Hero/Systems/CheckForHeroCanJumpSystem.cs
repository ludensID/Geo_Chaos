using Leopotam.EcsLite;
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
        .Inc<JumpAvailable>()
        .Inc<HeroVelocity>()
        .End();
    }
    
    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroVelocity velocity = ref _world.Get<HeroVelocity>(hero);
        ref JumpAvailable jump = ref _world.Get<JumpAvailable>(hero);
        if (velocity.Velocity.y <= 0)
          jump.IsJumping = false;
      }
    }
  }
}
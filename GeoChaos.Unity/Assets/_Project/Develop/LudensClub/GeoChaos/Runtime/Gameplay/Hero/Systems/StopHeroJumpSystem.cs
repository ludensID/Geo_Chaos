using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class StopHeroJumpSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public StopHeroJumpSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<StopJumpCommand>()
        .Inc<HeroVelocity>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroVelocity velocity = ref _world.Get<HeroVelocity>(hero);
        
        velocity.Velocity.y = _config.ForcedStopInertia;
        velocity.OverrideVelocityY = true;

        ref JumpAvailable jump = ref _world.Get<JumpAvailable>(hero);
        jump.IsJumping = false;

        _world.Del<StopJumpCommand>(hero);
      }
    }
  }
  
}
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
        .Inc<IsJumping>()
        .Inc<HeroMovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (int hero in _heroes)
      {
        ref HeroMovementVector vector = ref _world.Get<HeroMovementVector>(hero);
        vector.Speed.y = _config.ForcedStopInertia;

        _world.Del<IsJumping>(hero);
        _world.Del<StopJumpCommand>(hero);
      }
    }
  }
  
}
using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class CheckForHeroJumpStopSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public CheckForHeroJumpStopSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
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
      foreach (var hero in _heroes)
      {
        ref var velocity = ref _world.Get<HeroVelocity>(hero);

        if (velocity.Velocity.y <= 0)
          _world.Del<StopJumpCommand>(hero);
      }
    }
  }
}
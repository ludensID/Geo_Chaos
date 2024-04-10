using Leopotam.EcsLite;
using LudensClub.GeoChaos.Runtime.Configuration;
using LudensClub.GeoChaos.Runtime.Gameplay.Worlds;
using LudensClub.GeoChaos.Runtime.Utils;

namespace LudensClub.GeoChaos.Runtime.Gameplay.Core
{
  public class JumpHeroSystem : IEcsRunSystem
  {
    private readonly EcsWorld _world;
    private readonly EcsFilter _heroes;
    private readonly HeroConfig _config;

    public JumpHeroSystem(GameWorldWrapper gameWorldWrapper, IConfigProvider configProvider)
    {
      _world = gameWorldWrapper.World;
      _config = configProvider.Get<HeroConfig>();

      _heroes = _world.Filter<Hero>()
        .Inc<JumpAvailable>()
        .Inc<JumpCommand>()
        .Inc<MovementVector>()
        .End();
    }

    public void Run(EcsSystems systems)
    {
      foreach (var hero in _heroes)
      {
        ref var vector = ref _world.Get<MovementVector>(hero);
        vector.Speed.y = _config.JumpForce;
        vector.Direction.y = 1;

        _world.Add<IsJumping>(hero);

        _world.Del<JumpCommand>(hero);
      }
    }
  }
}